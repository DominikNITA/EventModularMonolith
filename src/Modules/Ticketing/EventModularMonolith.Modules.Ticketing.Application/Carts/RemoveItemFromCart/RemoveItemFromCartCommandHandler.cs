using EventModularMonolith.Modules.Ticketing.Domain.Customers;
using EventModularMonolith.Modules.Ticketing.Domain.TicketTypes;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Ticketing.Application.Carts.RemoveItemFromCart;

internal sealed class RemoveItemFromCartCommandHandler(
    ICustomerRepository customerRepository,
    ITicketTypeRepository ticketTypeRepository,
    CartService cartService)
    : ICommandHandler<RemoveItemFromCartCommand>
{
    public async Task<Result> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetByIdAsync(new CustomerId(request.CustomerId), cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        TicketType? ticketType = await ticketTypeRepository.GetByIdAsync(new TicketTypeId(request.TicketTypeId), cancellationToken);

        if (ticketType is null)
        {
            return Result.Failure(TicketTypeErrors.NotFound(request.TicketTypeId));
        }

        await cartService.RemoveItemAsync(customer.Id.Value, ticketType.Id.Value, cancellationToken);

        return Result.Success();
    }
}
