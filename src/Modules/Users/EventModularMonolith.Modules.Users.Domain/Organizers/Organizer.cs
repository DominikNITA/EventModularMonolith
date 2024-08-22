using EventModularMonolith.Modules.Users.Domain.Organizers.Events;
using EventModularMonolith.Modules.Users.Domain.Users;
using EventModularMonolith.Shared.Domain;
#pragma warning disable CA1806
#pragma warning disable S2201

namespace EventModularMonolith.Modules.Users.Domain.Organizers;

public sealed class Organizer : Entity
{
   private Organizer()
   {
   }

   public OrganizerId Id { get; set; }
   public string Name { get; private set; }
   public string Description { get; private set; }
   public OrganizerStatus Status { get; set; }
   public UserId OwnerId { get; private set; }
   public IEnumerable<Moderator> Moderators { get; private set; }

   public static Organizer Create(string name, string description, UserId owner)
   {
      var organizer = new Organizer
      {
         Id = new OrganizerId(Guid.NewGuid()),
         Name = name,
         Description = description,
         OwnerId = owner,
         Status = OrganizerStatus.Unverified,
      };
      organizer.Moderators = [Moderator.Create(organizer.Id, owner, ModeratorRole.Owner)];

      organizer.Raise(new OrganizerCreatedDomainEvent(organizer.Id));

      return organizer;
   }

   public Result Verify()
   {
      if (Status != OrganizerStatus.Unverified)
      {
         return Result.Failure(OrganizerErrors.OrganizerNotUnverified());
      }

      Status = OrganizerStatus.Verified;

      Raise(new OrganizerVerifiedDomainEvent(Id));

      return Result.Success();
   }

   public void Update(string name, string description)
   {
      if (Name == name && Description == description)
      {
         return;
      }

      Name = name;
      Description = description;
   }

   public Result AddModerator(User newModerator)
   {
      if (Moderators.Any(m => m.IsModerating(newModerator.Id)))
      {
         return Result.Failure(OrganizerErrors.UserIsAlreadyAModerator());
      }

      Moderators.Append(Moderator.Create(Id, newModerator.Id, ModeratorRole.Moderator));

      return Result.Success();
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


