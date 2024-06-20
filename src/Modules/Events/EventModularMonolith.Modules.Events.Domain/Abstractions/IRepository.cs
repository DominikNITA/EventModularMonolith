using EventModularMonolith.Modules.Events.Domain.Categories;

namespace EventModularMonolith.Modules.Events.Domain.Abstractions;

public interface IRepository<T> where T : Entity
{
   Task<T?> GetAsync(Guid id, CancellationToken cancellationToken = default);

   void Insert(T entity);
}
