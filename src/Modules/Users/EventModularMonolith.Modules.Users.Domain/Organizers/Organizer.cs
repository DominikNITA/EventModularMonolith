using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Domain.Organizers;

public sealed class Organizer : Entity
{
   private Organizer()
   {
   }

   public OrganizerId Id { get; set; }
   public string Name { get; private set; }
   public string Description { get; private set; }

   public static Organizer Create(string name, string description)
   {
      var user = new Organizer
      {
         Id = new OrganizerId(Guid.NewGuid()),
         Name = name,
         Description = description,
      };

      return user;
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
}

public class OrganizerId(Guid value) : TypedIdValueBase(value) { }


