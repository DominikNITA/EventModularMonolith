using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Ticketing.Application.Carts.GetCart;

public sealed record GetCartQuery(Guid CustomerId) : IQuery<Cart>;
