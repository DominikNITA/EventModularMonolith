using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.Organizers;

public class Moderator : Entity
{
   public OrganizerId OrganizerId { get; private set; }

   public ModeratorId UserId { get; private set; }

   public ModeratorRole Role { get; private set; }

   public bool IsActive { get; private set; }

   private Moderator()
   {

   }

   private Moderator(OrganizerId organizerId, ModeratorId userId, ModeratorRole role)
   {
      OrganizerId = organizerId;
      UserId = userId;
      Role = role;
      IsActive = true;
   }

   internal static Moderator Create(OrganizerId organizerId, ModeratorId userId, ModeratorRole role)
   {
      return new Moderator(organizerId, userId, role);
   }

   internal bool IsModerating(ModeratorId userId)
   {
      return IsActive && UserId == userId;
   }

   internal bool IsOwner(ModeratorId userId)
   {
      return UserId == userId && Role == ModeratorRole.Owner;
   }
}

public class ModeratorId(Guid value) : TypedIdValueBase(value) { }
