using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Ticketing.Application.Carts.RemoveItemFromCart;

public sealed record RemoveItemFromCartCommand(Guid CustomerId, Guid TicketTypeId) : ICommand;
