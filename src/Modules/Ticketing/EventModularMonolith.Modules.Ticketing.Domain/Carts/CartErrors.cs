using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Ticketing.Domain.Carts;

public static class CartErrors
{
   public static Error NotFound(Guid cartId) =>
      Error.NotFound("Carts.NotFound", $"The cart with the identifier {cartId} was not found");
}
