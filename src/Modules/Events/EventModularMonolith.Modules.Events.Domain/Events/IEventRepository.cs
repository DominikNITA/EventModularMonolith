namespace EventModularMonolith.Modules.Events.Domain.Events;

public interface IEventRepository
{
   Task InsertAsync(Event @event, CancellationToken cancellationToken = default);

   Task<Event> GetByIdAsync(EventId id, CancellationToken cancellationToken = default);
}
