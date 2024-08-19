using EventModularMonolith.Modules.Ticketing.Domain.Customers;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Ticketing.Application.Carts.ClearCart;

internal sealed class ClearCartCommandHandler(ICustomerRepository customerRepository, CartService cartService)
    : ICommandHandler<ClearCartCommand>
{
    public async Task<Result> Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetByIdAsync(new CustomerId(request.CustomerId), cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        await cartService.ClearAsync(customer.Id.Value, cancellationToken);

        return Result.Success();
    }
}
