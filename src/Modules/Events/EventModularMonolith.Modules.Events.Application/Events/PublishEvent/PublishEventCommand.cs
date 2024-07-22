using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Events.Application.Events.PublishEvent;

public sealed record PublishEventCommand(
   Guid EventId) : ICommand;
