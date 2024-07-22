using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.TicketTypes.Events;

public sealed class TicketTypePriceChangedDomainEvent(Guid ticketTypeId, decimal price) : DomainEvent
{
   public Guid TicketTypeId { get; init; } = ticketTypeId;

   public decimal Price { get; init; } = price;
}
