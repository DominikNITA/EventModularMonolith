// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Security.Claims;
using EventModularMonolith.Modules.Events.Application.Venues.CreateVenue;
using EventModularMonolith.Modules.Events.Application.Venues.DTOs;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Infrastructure.Authentication;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Events.Presentation.Venues;

internal sealed class CreateVenue : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPost("venues", async (CreateVenueRequest request, ClaimsPrincipal claims, ISender sender) =>
         {
            if (claims.GetUserOrganizerId() != request.OrganizerId)
            {
               return Results.Unauthorized();
            }

            var command = new CreateVenueCommand(
                    request.OrganizerId,
                    request.Name,
                    request.Description,
                    request.Address
            )
            {
               ImageContainers = request.ImageContainers
            };

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
         })
      .WithTags(Tags.Venues)
      .Produces<Result<Guid>>();
   }

   internal sealed class CreateVenueRequest
   {
      public Guid OrganizerId { get; set; }
      public string Name { get; set; } = string.Empty;
      public string Description { get; set; } = string.Empty;
      public AddressDto Address { get; set; }
      public IEnumerable<string> ImageContainers { get; set; }
   }
}
