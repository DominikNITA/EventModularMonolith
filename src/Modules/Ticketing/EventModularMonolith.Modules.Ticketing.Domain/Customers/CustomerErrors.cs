using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Ticketing.Domain.Customers;

public static class CustomerErrors
{
   public static Error NotFound(Guid customerId) =>
      Error.NotFound("Customers.NotFound", $"The customer with the identifier {customerId} was not found");
}
