using EventModularMonolith.Modules.Ticketing.Application.Abstractions.Authentication;
using EventModularMonolith.Shared.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace EventModularMonolith.Modules.Ticketing.Infrastructure.Authentication;

internal sealed class CustomerContext() : ICustomerContext
{
   public Guid CustomerId => Guid.NewGuid();
}
