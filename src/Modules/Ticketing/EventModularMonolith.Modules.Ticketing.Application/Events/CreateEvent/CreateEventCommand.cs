// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Ticketing.Application.Events.CreateEvent;

public sealed record CreateEventCommand(
   Guid EventId,
   string Title,
   string Description,
   string Location,
   DateTime StartsAtUtc,
   DateTime? EndsAtUtc,
   List<CreateEventCommand.TicketTypeRequest> TicketTypes
) : ICommand
{
   public sealed record TicketTypeRequest(
      Guid TicketTypeId,
      Guid EventId,
      string Name,
      decimal Price,
      string Currency,
      decimal Quantity);
}




