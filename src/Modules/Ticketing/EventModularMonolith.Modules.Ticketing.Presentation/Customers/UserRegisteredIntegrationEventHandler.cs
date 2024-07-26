using EventModularMonolith.Modules.Ticketing.Application.Customers.CreateCustomer;
using EventModularMonolith.Modules.Users.IntegrationEvents;
using EventModularMonolith.Shared.Application.EventBus;
using EventModularMonolith.Shared.Application.Exceptions;
using EventModularMonolith.Shared.Domain;
using MediatR;

namespace EventModularMonolith.Modules.Ticketing.Presentation.Customers;

internal sealed class UserRegisteredIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public override async Task Handle(
        UserRegisteredIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateCustomerCommand(
                integrationEvent.UserId,
                integrationEvent.Email,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new GeneralException(nameof(CreateCustomerCommand), result.Error);
        }
    }
}
