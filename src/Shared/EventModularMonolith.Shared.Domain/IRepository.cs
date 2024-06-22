namespace EventModularMonolith.Shared.Domain;

public interface IRepository<T> where T : Entity
{
   Task<T?> GetAsync(Guid id, CancellationToken cancellationToken = default);

   void Insert(T entity);
}
