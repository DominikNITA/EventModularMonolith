using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Shared.Application.Clock;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Application.Events.RescheduleEvent;

internal sealed class RescheduleEventCommandHandler(
   IEventRepository eventRepository,
   IDateTimeProvider dateTimeProvider,
   IUnitOfWork unitOfWork) : ICommandHandler<RescheduleEventCommand>
{
   public async Task<Result> Handle(RescheduleEventCommand request, CancellationToken cancellationToken)
   {
      Event @event = await eventRepository.GetByIdAsync(new EventId(request.EventId), cancellationToken);

      if (@event == null)
      {
         return Result.Failure(EventErrors.NotFound(request.EventId));
      }

      if (request.StartsAtUtc < dateTimeProvider.UtcNow)
      {
         return Result.Failure(EventErrors.StartDateInPast);
      }

      @event.Reschedule(request.StartsAtUtc, request.EndsAtUtc);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return Result.Success();
   }
}
