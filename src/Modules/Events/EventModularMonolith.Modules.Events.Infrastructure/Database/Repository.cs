using EventModularMonolith.Modules.Events.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Events.Infrastructure.Database;

internal class Repository<T>(EventsDbContext context) : IRepository<T> where T : Entity
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
