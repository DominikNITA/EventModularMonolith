using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Domain.Users;

public sealed class User : Entity
{
   private User()
   {
   }

   public UserId Id { get; set; }
   public string Email { get; private set; }

   public string FirstName { get; private set; }

   public string LastName { get; private set; }
   public string IdentityId { get; private set; }

   public static User Create(string email, string firstName, string lastName, string identityId)
   {
      var user = new User
      {
         Id = new UserId(Guid.NewGuid()),
         Email = email,
         FirstName = firstName,
         LastName = lastName,
         IdentityId = identityId
      };

      user.Raise(new UserRegisteredDomainEvent(user.Id.Value));

      return user;
   }

   public void Update(string firstName, string lastName)
   {
      if (FirstName == firstName && LastName == lastName)
      {
         return;
      }

      FirstName = firstName;
      LastName = lastName;

      Raise(new UserProfileUpdatedDomainEvent(Id, FirstName, LastName));
   }
}

public class UserId(Guid value) : TypedIdValueBase(value) { }
