using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Modules.Events.Domain.TicketTypes;
using EventModularMonolith.Shared.Application.Clock;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Application.Events.PublishEvent;

internal sealed class PublishEventCommandHandler(
   IEventRepository eventRepository,
   ITicketTypeRepository ticketTypeRepository,
   IUnitOfWork unitOfWork) : ICommandHandler<PublishEventCommand>
{
   public async Task<Result> Handle(PublishEventCommand request, CancellationToken cancellationToken)
   {
      Event @event = await eventRepository.GetAsync(request.EventId, cancellationToken);

      if (@event == null)
      {
         return Result.Failure(EventErrors.NotFound(request.EventId));
      }

      if (!await ticketTypeRepository.ExistsAsync(@event.Id, cancellationToken))
      {
         return Result.Failure(EventErrors.NoTicketsFound);
      }

      Result result = @event.Publish();

      if (result.IsFailure)
      {
         return Result.Failure(result.Error);
      }

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return Result.Success();
   }
}
