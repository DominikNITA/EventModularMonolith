using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Modules.Events.Domain.Speakers;
using EventModularMonolith.Modules.Events.Domain.Venues;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.Organizers;

public sealed class Organizer : Entity
{
   private Organizer()
   {
   }

   public OrganizerId Id { get; set; }
   public string Name { get; private set; }
   public string Description { get; private set; }
   public OrganizerStatus Status { get; set; }
   public ModeratorId OwnerId { get; private set; }
   public IEnumerable<Moderator> Moderators { get; private set; }

   public static Organizer Create(Guid organizerId, string name, string description, ModeratorId owner)
   {
      var organizer = new Organizer
      {
         Id = new OrganizerId(organizerId),
         Name = name,
         Description = description,
         OwnerId = owner,
         Status = OrganizerStatus.Unverified,
      };
      organizer.Moderators = [Moderator.Create(organizer.Id, owner, ModeratorRole.Owner)];

      return organizer;
   }

   public Result<Event> CreateEvent(
      Category category, string title, string description, VenueId venueId, DateTime startsAtUtc, DateTime? endsAtUtc,
      List<Speaker> speakers
   )
   {
      Result<Event> @event = Event.Create(Id, category, title, description, venueId, startsAtUtc, endsAtUtc, speakers);

      if (@event.IsFailure)
      {
         return Result.Failure<Event>(@event.Error);
      }

      return @event;
   }
}

public enum OrganizerStatus
{
   Unverified = 0,
   Verified = 1,
   Hidden = 2,
   Archived = 3,
}

public class OrganizerId(Guid value) : TypedIdValueBase(value) { }
