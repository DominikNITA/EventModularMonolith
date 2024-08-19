using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Modules.Events.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Events.Infrastructure.Events;

internal sealed class EventRepository(EventsDbContext context) : IEventRepository
{
   public async Task<Event> GetByIdAsync(EventId id, CancellationToken cancellationToken = default)
   {
      return await context.Events.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
   }

   public async Task InsertAsync(Event @event, CancellationToken cancellationToken = default)
   {
      await context.Set<Event>().AddAsync(@event, cancellationToken);
   }
}
