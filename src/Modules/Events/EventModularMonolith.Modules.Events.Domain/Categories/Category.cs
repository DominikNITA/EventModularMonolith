using EventModularMonolith.Modules.Events.Domain.Categories.Events;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.Categories;

public sealed class Category : Entity
{
   private Category() { }

   public CategoryId Id { get; set; }

   public string Name { get; private set; }
   public bool IsArchived { get; private set; }

   public static Category Create(string name)
   {
      var category = new Category
      {
         Id = new CategoryId(Guid.NewGuid()),
         Name = name,
         IsArchived = false
      };

      category.Raise(new CategoryCreatedDomainEvent(category.Id));

      return category;
   }

   public void Update(string name)
   {
      if (Name == name)
      {
         return;
      }

      Name = name;

      Raise(new CategoryUpdatedDomainEvent(Id, Name));
   }
}

public class CategoryId(Guid value) : TypedIdValueBase(value) { }
