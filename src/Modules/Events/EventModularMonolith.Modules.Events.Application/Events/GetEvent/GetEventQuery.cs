using EventModularMonolith.Modules.Events.Application.Abstractions.Messaging;
using MediatR;

namespace EventModularMonolith.Modules.Events.Application.Events.GetEvent;

public sealed record GetEventQuery(Guid eventId) : IQuery<EventResponse>;
