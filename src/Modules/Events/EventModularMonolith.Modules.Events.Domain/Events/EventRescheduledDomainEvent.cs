using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.Events;

public class EventRescheduledDomainEvent(EventId eventId, DateTime startsAtUtc, DateTime? endsAtUtc) : DomainEvent
{
   public EventId EventId { get; init; } = eventId;
   public DateTime StartsAtUtc { get; init; } = startsAtUtc;
   public DateTime? EndsAtUtc { get; init; } = endsAtUtc;
}
