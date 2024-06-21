using EventModularMonolith.Modules.Events.Domain.Abstractions;

namespace EventModularMonolith.Modules.Events.Domain.Events;

public class EventPublishedDomainEvent(Guid eventId) : DomainEvent
{
   public Guid EventId { get; init; } = eventId;
}