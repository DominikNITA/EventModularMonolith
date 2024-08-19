using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.TicketTypes.Events;

public sealed class TicketTypePriceChangedDomainEvent(TicketTypeId ticketTypeId, decimal price) : DomainEvent
{
   public TicketTypeId TicketTypeId { get; init; } = ticketTypeId;

   public decimal Price { get; init; } = price;
}
