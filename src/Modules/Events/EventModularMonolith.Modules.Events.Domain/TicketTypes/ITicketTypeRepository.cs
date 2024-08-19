using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.TicketTypes;

public interface ITicketTypeRepository
{
   Task InsertAsync(TicketType ticketType, CancellationToken cancellationToken = default);

   Task<TicketType> GetByIdAsync(TicketTypeId id, CancellationToken cancellationToken = default);

   Task<bool> ExistsAsync(EventId eventId, CancellationToken cancellationToken = default);
}
