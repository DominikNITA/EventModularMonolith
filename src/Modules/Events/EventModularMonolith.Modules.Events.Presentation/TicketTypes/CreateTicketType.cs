// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.TicketTypes.CreateTicketType;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Events.Presentation.TicketTypes;

internal sealed class CreateTicketType : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPost("ticket-types", async (CreateTicketTypeRequest request, ISender sender) =>
         {
            var command = new CreateTicketTypeCommand(
               request.EventId,
               request.Name,
               request.Price,
               request.Currency,
               request.Quantity
            );

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
         })
      .WithTags(Tags.TicketTypes);
   }

   internal sealed class CreateTicketTypeRequest
   {
      public Guid EventId { get; set; }
      public string Name { get; set; } = string.Empty;
      public decimal Price { get; set; }
      public string Currency { get; set; }
      public decimal Quantity { get; set; }

   }
}
