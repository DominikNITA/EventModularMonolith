using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Ticketing.Domain.Customers;

public sealed class Customer : Entity
{
   private Customer()
   {
   }

   public string Email { get; private set; }

   public string FirstName { get; private set; }

   public string LastName { get; private set; }

   public static Customer Create(Guid id, string email, string firstName, string lastName)
   {
      var user = new Customer
      {
         Id = id,
         Email = email,
         FirstName = firstName,
         LastName = lastName,
      };

      return user;
   }

   public void Update(string firstName, string lastName)
   {
      FirstName = firstName;
      LastName = lastName;
   }
}
