using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Events.Application.Events.CreateEvent;

public sealed record CreateEventCommand(
   Guid CategoryId,
   string Title,
   string Description,
   Guid VenueId,
   DateTime StartsAtUtc,
   DateTime? EndsAtUtc) : ICommand<Guid>;
