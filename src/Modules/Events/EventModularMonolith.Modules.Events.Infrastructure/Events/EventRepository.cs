using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Modules.Events.Infrastructure.Database;

namespace EventModularMonolith.Modules.Events.Infrastructure.Events;

internal sealed class EventRepository(EventsDbContext context) : IEventRepository
{
   public void Insert(Event @event)
   {
      context.Events.Add(@event);
   }
}
