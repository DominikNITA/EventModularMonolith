// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using EventModularMonolith.Modules.Events.Application.Events;
using EventModularMonolith.Shared.Infrastructure.Authentication;
using EventModularMonolith.Modules.Events.Application.Organizers.GetEventsForOrganizer;
using EventModularMonolith.Modules.Events.Application.Organizers.GetVenuesForOrganizer;
using EventModularMonolith.Modules.Events.Application.Venues.DTOs;
using EventModularMonolith.Modules.Events.Domain.Organizers;

namespace EventModularMonolith.Modules.Events.Presentation.Organizers;

internal sealed class GetVenuesForOrganizer : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapGet("organizers/{organizerId}/venues", async (Guid organizerId, ClaimsPrincipal claims, ISender sender) =>
         {
            if (claims.GetUserOrganizerId() != organizerId)
            {
               return Results.Unauthorized();
            }

            Result<IReadOnlyCollection<VenueGridDto>> result = await sender.Send(new GetVenuesForOrganizerQuery(claims.GetUserOrganizerId()));

            return result.Match(Results.Ok, ApiResults.Problem);
         })
      .WithTags(Tags.Organizers)
      .Produces<Result<IReadOnlyCollection<VenueGridDto>>>()
      .WithName("GetVenuesForOrganizer");
   }
}
