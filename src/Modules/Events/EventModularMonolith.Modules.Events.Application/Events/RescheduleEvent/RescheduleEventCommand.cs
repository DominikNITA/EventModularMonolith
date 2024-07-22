using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Events.Application.Events.RescheduleEvent;

public sealed record RescheduleEventCommand(
   Guid EventId,
   DateTime StartsAtUtc,
   DateTime? EndsAtUtc) : ICommand;
