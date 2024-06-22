using EventModularMonolith.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Shared.Infrastructure.Database;

public class Repository<T>(DbContext context) : IRepository<T> where T : Entity
{
   public async Task<T?> GetAsync(Guid id, CancellationToken cancellationToken = default)
   {
      return await context.Set<T>().SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
   }

   public void Insert(T entity)
   {
      context.Set<T>().Add(entity);
   }
}
