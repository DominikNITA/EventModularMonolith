using EventModularMonolith.Modules.Events.Domain.TicketTypes;
using EventModularMonolith.Modules.Events.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Events.Infrastructure.TicketTypes;

internal sealed class TicketTypeRepository(EventsDbContext context) : Repository<TicketType>(context), ITicketTypeRepository
{
   public async Task<bool> ExistsAsync(Guid eventId, CancellationToken cancellationToken = default)
   {
      return await context.TicketTypes.AnyAsync(t => t.EventId == eventId, cancellationToken);
   }
}
