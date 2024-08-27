using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Shared.Application.Clock;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Application.Events.CancelEvent;

internal sealed class CancelEventCommandHandler(
   IEventRepository eventRepository,
   IDateTimeProvider dateTimeProvider,
   IUnitOfWork unitOfWork) : ICommandHandler<CancelEventCommand>
{
   public async Task<Result> Handle(CancelEventCommand request, CancellationToken cancellationToken)
   {
      Event @event = await eventRepository.GetByIdAsync(new EventId(request.EventId), cancellationToken);

      if (@event == null)
      {
         return Result.Failure(EventErrors.NotFound(request.EventId));
      }

      Result result = @event.Cancel(dateTimeProvider.UtcNow);

      if (result.IsFailure)
      {
         return Result.Failure(result.Error);
      }

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return Result.Success();
   }
}
