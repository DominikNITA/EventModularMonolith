﻿using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Modules.Events.Domain.TicketTypes.Events;

namespace EventModularMonolith.Modules.Events.Domain.TicketTypes;

public sealed class TicketType : Entity
{
   private TicketType()
   {
   }

   public TicketTypeId Id { get; set; }
   public EventId EventId { get; private set; }

   public string Name { get; private set; }

   //TODO: Create money value object
   public decimal Price { get; private set; }

   public string Currency { get; private set; }

   public decimal Quantity { get; private set; }

   public static TicketType Create(
       Event @event,
       string name,
       decimal price,
       string currency,
       decimal quantity)
   {
      var ticketType = new TicketType
      {
         Id = new TicketTypeId(Guid.NewGuid()),
         EventId = @event.Id,
         Name = name,
         Price = price,
         Currency = currency,
         Quantity = quantity
      };

      ticketType.Raise(new TicketTypeCreatedDomainEvent(ticketType.Id));

      return ticketType;
   }

   public void UpdatePrice(decimal price)
   {
      if (Price == price)
      {
         return;
      }

      Price = price;

      Raise(new TicketTypePriceChangedDomainEvent(Id, Price));
   }
}

public class TicketTypeId(Guid value) : TypedIdValueBase(value) { }
