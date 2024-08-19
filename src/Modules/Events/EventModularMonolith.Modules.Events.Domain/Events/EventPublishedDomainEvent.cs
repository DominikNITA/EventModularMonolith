using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.Events;

public class EventPublishedDomainEvent(EventId eventId) : DomainEvent
{
   public EventId EventId { get; init; } = eventId;
}
