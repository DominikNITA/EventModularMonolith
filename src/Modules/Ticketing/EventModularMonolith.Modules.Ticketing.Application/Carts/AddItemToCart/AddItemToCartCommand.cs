using EventModularMonolith.Modules.Ticketing.Domain.Customers;
using EventModularMonolith.Modules.Users.PublicApi;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;
using FluentValidation;

namespace EventModularMonolith.Modules.Ticketing.Application.Carts.AddItemToCart;

public record AddItemToCartCommand(Guid CustomerId, Guid TicketTypeId, decimal Quantity) : ICommand;

internal sealed class AddItemToCartCommandValidator : AbstractValidator<AddItemToCartCommand>
{
   public AddItemToCartCommandValidator()
   {
      RuleFor(x => x.CustomerId).NotEmpty();
      RuleFor(x => x.TicketTypeId).NotEmpty();
      RuleFor(x => x.Quantity).GreaterThan(decimal.Zero);
   }
}

internal sealed class AddItemToCartCommandHandler(IUsersApi usersApi, CartService cartService) : ICommandHandler<AddItemToCartCommand>
{
   public async Task<Result> Handle(AddItemToCartCommand command, CancellationToken cancellationToken)
   {
      UserResponse? customer = await usersApi.GetUserAsync(command.CustomerId, cancellationToken);

      if (customer is null)
      {
         return Result.Failure(CustomerErrors.NotFound(command.CustomerId));
      }


   }
}

