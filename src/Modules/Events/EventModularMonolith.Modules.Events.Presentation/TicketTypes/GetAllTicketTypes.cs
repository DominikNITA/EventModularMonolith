// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.TicketTypes.GetAllTicketTypes;
using EventModularMonolith.Modules.Events.Application.TicketTypes.DTOs;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Events.Presentation.TicketTypes;

internal sealed class GetAllTicketTypes : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapGet("ticket-types", async (ISender sender) =>
         {
            Result<IReadOnlyCollection<TicketTypeDto>> result = await sender.Send(new GetAllTicketTypesQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
         })
      .WithTags(Tags.TicketTypes);
   }
}
