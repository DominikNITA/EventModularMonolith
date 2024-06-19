using MediatR;

namespace EventModularMonolith.Modules.Events.Application.Events.GetEvent;

public sealed record GetEventQuery(Guid eventId) : IRequest<EventResponse?>;
