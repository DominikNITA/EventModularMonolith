using EventModularMonolith.Modules.Events.Domain.Abstractions;

namespace EventModularMonolith.Modules.Events.Domain.TicketTypes;

public interface ITicketTypeRepository : IRepository<TicketType>
{
   Task<bool> ExistsAsync(Guid eventId, CancellationToken cancellationToken = default);
}
