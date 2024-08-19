using EventModularMonolith.Modules.Ticketing.Domain.Events;
using EventModularMonolith.Modules.Ticketing.Domain.TicketTypes.Events;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Ticketing.Domain.TicketTypes;

public sealed class TicketType : Entity
{
   private TicketType()
   {
   }

   public TicketTypeId Id { get; set; }
   public EventId EventId { get; private set; }

   public string Name { get; private set; }

   public decimal Price { get; private set; }

   public string Currency { get; private set; }

   public decimal Quantity { get; private set; }

   public decimal AvailableQuantity { get; private set; }

   public static TicketType Create(
      Guid id,
      EventId eventId,
      string name,
      decimal price,
      string currency,
      decimal quantity)
   {
      var ticketType = new TicketType
      {
         Id = new TicketTypeId(id),
         EventId = eventId,
         Name = name,
         Price = price,
         Currency = currency,
         Quantity = quantity,
         AvailableQuantity = quantity
      };

      return ticketType;
   }

   public void UpdatePrice(decimal price)
   {
      Price = price;
   }

   public Result UpdateQuantity(decimal quantity)
   {
      if (AvailableQuantity < quantity)
      {
         return Result.Failure(TicketTypeErrors.NotEnoughQuantity(AvailableQuantity));
      }

      AvailableQuantity -= quantity;

      if (AvailableQuantity == 0)
      {
         Raise(new TicketTypeSoldOutDomainEvent(Id));
      }

      return Result.Success();
   }
}

public class TicketTypeId(Guid value) : TypedIdValueBase(value) { }
