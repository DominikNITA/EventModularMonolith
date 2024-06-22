using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Modules.Events.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Database;

namespace EventModularMonolith.Modules.Events.Infrastructure.Events;

internal sealed class EventRepository(EventsDbContext context) : Repository<Event>(context), IEventRepository
{

}
