using EventModularMonolith.Modules.Ticketing.Domain.Customers;
using EventModularMonolith.Modules.Ticketing.Domain.TicketTypes;
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

internal sealed class AddItemToCartCommandHandler(
   IUsersApi usersApi,
   ITicketTypeRepository ticketTypeRepository,
   CartService cartService) : ICommandHandler<AddItemToCartCommand>
{
   public async Task<Result> Handle(AddItemToCartCommand command, CancellationToken cancellationToken)
   {
      UserResponse? customer = await usersApi.GetUserAsync(command.CustomerId, cancellationToken);

      if (customer is null)
      {
         return Result.Failure(CustomerErrors.NotFound(command.CustomerId));
      }

      TicketType? ticketType = await ticketTypeRepository.GetByIdAsync(new TicketTypeId(command.TicketTypeId), cancellationToken);

      if (ticketType is null)
      {
         return Result.Failure(TicketTypeErrors.NotFound(command.TicketTypeId));
      }

      if (ticketType.AvailableQuantity < command.Quantity)
      {
         return Result.Failure(TicketTypeErrors.NotEnoughQuantity(ticketType.AvailableQuantity));
      }

      var cartItem = new CartItem
      {
         TicketTypeId = command.TicketTypeId,
         Quantity = command.Quantity,
         Price = ticketType.Price,
         Currency = ticketType.Currency
      };

      await cartService.AddItemAsync(customer.Id, cartItem, cancellationToken);

      return Result.Success();
   }
}

