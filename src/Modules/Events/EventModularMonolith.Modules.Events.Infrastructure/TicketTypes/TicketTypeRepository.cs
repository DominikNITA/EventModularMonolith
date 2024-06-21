using EventModularMonolith.Modules.Events.Domain.TicketTypes;
using EventModularMonolith.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Events.Infrastructure.TicketTypes;

#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
internal sealed class TicketTypeRepository(EventsDbContext context) : Repository<TicketType>(context), ITicketTypeRepository
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
{
   public async Task<bool> ExistsAsync(Guid eventId, CancellationToken cancellationToken = default)
   {
      return await context.TicketTypes.AnyAsync(t => t.EventId == eventId, cancellationToken);
   }
}
