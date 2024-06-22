﻿using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.TicketTypes;

public static class TicketTypeErrors
{
   public static Error NotFound(Guid ticketTypeId) =>
       Error.NotFound("TicketTypes.NotFound", $"The ticket type with the identifier {ticketTypeId} was not found");
}
