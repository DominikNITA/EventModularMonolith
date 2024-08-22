using EventModularMonolith.Modules.Users.Domain.Users;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Domain.Organizers;

public class Moderator : Entity
{
   public OrganizerId OrganizerId { get; private set; }

   public UserId UserId { get; private set; }

   public ModeratorRole Role { get; private set; }

   public bool IsActive { get; private set; }

   private Moderator()
   {
      
   }

   private Moderator(OrganizerId organizerId, UserId userId, ModeratorRole role)
   {
      OrganizerId = organizerId;
      UserId = userId;
      Role = role;
      IsActive = true;
   }

   internal static Moderator Create(OrganizerId organizerId, UserId userId, ModeratorRole role)
   {
      return new Moderator(organizerId, userId, role);
   }

   internal bool IsModerating(UserId userId)
   {
      return IsActive && UserId == userId;
   }

   internal bool IsOwner(UserId userId)
   {
      return UserId == userId && Role == ModeratorRole.Owner;
   }
}
