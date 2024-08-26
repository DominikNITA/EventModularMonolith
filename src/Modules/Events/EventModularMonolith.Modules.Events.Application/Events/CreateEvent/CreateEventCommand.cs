using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Events.Application.Events.CreateEvent;

public sealed record CreateEventCommand(
   Guid OrganizerId,
   Guid CategoryId,
   string Title,
   string Description,
   Guid VenueId,
   DateTime StartsAtUtc,
   DateTime? EndsAtUtc,
   IEnumerable<Guid> SpeakerIds) : ICommand<Guid>
{
   public IEnumerable<string> ImageContainers { get; set; }
}
