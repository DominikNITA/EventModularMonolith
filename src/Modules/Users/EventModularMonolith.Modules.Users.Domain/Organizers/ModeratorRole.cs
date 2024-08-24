using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Domain.Organizers;


public class ModeratorRole : ValueObject
{
   public static ModeratorRole Owner => new ModeratorRole("Owner");
   public static ModeratorRole Moderator => new ModeratorRole("Moderator");

   public string Value { get; }

   private ModeratorRole(string value)
   {
      Value = value;
   }

   public static ModeratorRole Of(string roleCode)
   {
      return new ModeratorRole(roleCode);
   }
}

