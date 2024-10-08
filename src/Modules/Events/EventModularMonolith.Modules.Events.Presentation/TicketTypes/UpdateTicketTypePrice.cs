﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Events.Presentation.TicketTypes;

internal sealed class UpdateTicketTypePrice : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPut("ticket-types/{id}", async (Guid id, UpdateTicketTypePriceRequest request, ISender sender) =>
         {
            var command = new UpdateTicketTypePriceCommand(
               id,
               request.Price
            );

            Result result = await sender.Send(command);

            return result.Match(() => Results.Ok(), ApiResults.Problem);
         })
      .WithTags(Tags.TicketTypes);
   }

   internal sealed class UpdateTicketTypePriceRequest
   {
      public decimal Price { get; set; }
   }
}
