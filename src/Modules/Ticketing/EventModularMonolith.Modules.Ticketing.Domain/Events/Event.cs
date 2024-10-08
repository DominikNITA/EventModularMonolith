﻿using EventModularMonolith.Modules.Ticketing.Domain.Customers;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Ticketing.Domain.Events;

public sealed class Event : Entity
{
   private Event()
   {
   }

   public EventId Id { get; set; }
   public CustomerId CustomerId { get; set; }

   public string Title { get; private set; }

   public string Description { get; private set; }

   public string Location { get; private set; }

   public DateTime StartsAtUtc { get; private set; }

   public DateTime? EndsAtUtc { get; private set; }

   public bool Canceled { get; private set; }

   public static Event Create(
      Guid id,
      string title,
      string description,
      string location,
      DateTime startsAtUtc,
      DateTime? endsAtUtc)
   {
      var @event = new Event
      {
         Id = new EventId(id),
         Title = title,
         Description = description,
         Location = location,
         StartsAtUtc = startsAtUtc,
         EndsAtUtc = endsAtUtc
      };

      return @event;
   }
}

public class EventId(Guid value) : TypedIdValueBase(value) { }
