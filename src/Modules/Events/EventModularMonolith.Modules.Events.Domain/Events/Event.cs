﻿using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Domain.Organizers;
using EventModularMonolith.Modules.Events.Domain.Speakers;
using EventModularMonolith.Modules.Events.Domain.Venues;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.Events;

public sealed class Event : Entity
{
   private Event() { }

   public EventId Id { get; private set; }
   public OrganizerId OrganizerId { get; private set; }
   public CategoryId CategoryId { get; private set; }
   public string Title { get; private set; }
   public string Description { get; private set; }
   public VenueId VenueId { get; private set; }
   public DateTime StartsAtUtc { get; private set; }
   public DateTime? EndsAtUtc { get; private set; }
   public EventStatus Status { get; private set; }
   public List<Speaker> Speakers { get; set; } = [];

   internal static Result<Event> Create(OrganizerId organizerId, Category category, string title, string description, VenueId venueId, DateTime startsAtUtc, DateTime? endsAtUtc, List<Speaker> speakers)
   {
      if (endsAtUtc.HasValue && endsAtUtc < startsAtUtc)
      {
         return Result.Failure<Event>(EventErrors.EndDatePrecedesStartDate);
      }

      var @event = new Event()
      {
         Id = new EventId(Guid.NewGuid()),
         OrganizerId = organizerId,
         CategoryId = category.Id,
         Title = title,
         Description = description,
         VenueId = venueId,
         StartsAtUtc = startsAtUtc,
         EndsAtUtc = endsAtUtc,
         Status = EventStatus.Draft,
         Speakers = speakers
      };

      @event.Raise(new EventCreatedDomainEvent(@event.Id));

      return @event;
   }

   public Result Publish()
   {
      if (Status != EventStatus.Draft)
      {
         return Result.Failure(EventErrors.NotDraft);
      }

      Status = EventStatus.Published;

      Raise(new EventPublishedDomainEvent(Id));

      return Result.Success();
   }

   public void Reschedule(DateTime startsAtUtc, DateTime? endsAtUtc)
   {
      if (StartsAtUtc == startsAtUtc && EndsAtUtc == endsAtUtc)
      {
         return;
      }

      StartsAtUtc = startsAtUtc;
      EndsAtUtc = endsAtUtc;

      Raise(new EventRescheduledDomainEvent(Id, StartsAtUtc, EndsAtUtc));
   }

   public Result Cancel(DateTime utcNow)
   {
      if (Status == EventStatus.Canceled)
      {
         return Result.Failure(EventErrors.AlreadyCanceled);
      }

      if (StartsAtUtc < utcNow)
      {
         return Result.Failure(EventErrors.AlreadyStarted);
      }

      Status = EventStatus.Canceled;

      Raise(new EventCanceledDomainEvent(Id));

      return Result.Success();
   }
}


public class EventId(Guid value) : TypedIdValueBase(value) { }


