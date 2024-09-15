using EventModularMonolith.Modules.Events.Application.Events;
using EventModularMonolith.Modules.Events.Domain.Organizers;
using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Events.Application.Organizers.GetEventsForOrganizer;

public sealed record GetEventsForOrganizerQuery(Guid OrganizerId) : IQuery<IReadOnlyCollection<EventResponse>>;
