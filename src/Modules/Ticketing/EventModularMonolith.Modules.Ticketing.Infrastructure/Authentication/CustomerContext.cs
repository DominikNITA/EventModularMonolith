using EventModularMonolith.Modules.Ticketing.Application.Abstractions.Authentication;
using EventModularMonolith.Shared.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace EventModularMonolith.Modules.Ticketing.Infrastructure.Authentication;

internal sealed class CustomerContext(IHttpContextAccessor httpContextAccessor) : ICustomerContext
{
   public Guid CustomerId => Guid.NewGuid(); //httpContextAccessor.HttpContext?.User.GetUserId() ??
   //throw new GeneralException("User identifier is unavailable");
}
