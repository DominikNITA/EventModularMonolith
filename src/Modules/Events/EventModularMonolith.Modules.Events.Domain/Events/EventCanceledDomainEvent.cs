using EventModularMonolith.Modules.Events.Domain.Abstractions;

namespace EventModularMonolith.Modules.Events.Domain.Events;

public class EventCanceledDomainEvent(Guid eventId) : DomainEvent
{
   public Guid EventId { get; init; } = eventId;
}