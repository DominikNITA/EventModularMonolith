// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.Events;
using EventModularMonolith.Modules.Events.Application.Venues.CreateVenue;
using EventModularMonolith.Modules.Events.Application.Venues.DTOs;
using EventModularMonolith.Shared.Domain;
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
      app.MapPost("venues", async (CreateVenueRequest request, ISender sender) =>
         {
            var command = new CreateVenueCommand(
                    request.Name,
                    request.Description,
                    request.Address
            );

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
         })
      .WithTags(Tags.Venues)
      .Produces<Result<Guid>>();
   }

   internal sealed class CreateVenueRequest
   {
      public string Name { get; set; } = string.Empty;
      public string Description { get; set; } = string.Empty;
      public AddressDto Address { get; set; }
   }
}
