// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.Venues.GetVenue;
using EventModularMonolith.Modules.Events.Application.Venues.DTOs;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Events.Presentation.Venues;

internal sealed class GetVenue : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapGet("venues/{id}", async (Guid id, ISender sender) =>
         {
            Result<VenueDto> result = await sender.Send(new GetVenueQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
         })
      .WithTags(Tags.Venues)
      .Produces<Result<VenueDto>>()
      .WithName("GetVenue");
   }
}
