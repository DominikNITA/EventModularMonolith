using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Modules.Events.Domain.TicketTypes;
using EventModularMonolith.Modules.Events.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Events.Infrastructure.TicketTypes;


internal sealed class TicketTypeRepository(EventsDbContext context) : ITicketTypeRepository
{
   public async Task<TicketType> GetByIdAsync(TicketTypeId id, CancellationToken cancellationToken = default)
   {
      return await context.TicketTypes.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
   }

   public async Task InsertAsync(TicketType ticketType, CancellationToken cancellationToken = default)
   {
      await context.Set<TicketType>().AddAsync(ticketType, cancellationToken);
   }

   public async Task<bool> ExistsAsync(EventId eventId, CancellationToken cancellationToken = default)
   {
      return await context.TicketTypes.AnyAsync(t => t.EventId == eventId, cancellationToken);
   }
}
