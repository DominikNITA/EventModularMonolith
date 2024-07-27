using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Ticketing.Application.Carts.ClearCart;

public sealed record ClearCartCommand(Guid CustomerId) : ICommand;
