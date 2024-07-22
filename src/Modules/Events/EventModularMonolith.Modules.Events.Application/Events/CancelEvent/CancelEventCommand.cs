using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Events.Application.Events.CancelEvent;

public sealed record CancelEventCommand(
   Guid EventId) : ICommand;
