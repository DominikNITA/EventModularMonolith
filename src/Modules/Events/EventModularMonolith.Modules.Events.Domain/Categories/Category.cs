using EventModularMonolith.Modules.Events.Domain.Abstractions;

namespace EventModularMonolith.Modules.Events.Domain.Categories;

public sealed class Category : Entity
{
   private Category() { }

   public string Name { get; private set; }
   public bool IsArchived { get; private set; }

   public static Category Create(string name)
   {
      var category = new Category
      {
         Id = Guid.NewGuid(),
         Name = name,
         IsArchived = false
      };

      category.Raise(new CategoryCreatedDomainEvent(category.Id));

      return category;
   }
}

public sealed class CategoryCreatedDomainEvent(Guid categoryId) : DomainEvent
{
   public Guid CategoryId { get; init; } = categoryId;
}
