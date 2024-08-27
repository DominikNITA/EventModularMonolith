using EventModularMonolith.Modules.Ticketing.Application.Abstractions.Authentication;

namespace EventModularMonolith.Modules.Ticketing.Infrastructure.Authentication;

internal sealed class CustomerContext() : ICustomerContext
{
   public Guid CustomerId => Guid.NewGuid();
}
