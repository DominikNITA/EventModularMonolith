using EventModularMonolith.Modules.Events.Application.Abstractions.Clock;
using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Application.Abstractions.Messaging;
using EventModularMonolith.Modules.Events.Domain.Abstractions;
using EventModularMonolith.Modules.Events.Domain.Events;
using MediatR;

namespace EventModularMonolith.Modules.Events.Application.Events.CreateEvent;

internal sealed class CreateEventCommandHandler(
   IDateTimeProvider dateTimeProvider, 
   IEventRepository eventRepository, 
   IUnitOfWork unitOfWork) : ICommandHandler<CreateEventCommand, Guid>
{

   public async Task<Result<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
   {
      if (request.StartsAtUtc < dateTimeProvider.UtcNow)
      {
         return Result.Failure<Guid>(EventErrors.StartDateInPast);
      }

      Result<Event> result = Event.Create(
         request.Title,
         request.Description,
         request.Location,
         request.StartsAtUtc,
         request.EndsAtUtc);

      if (result.IsFailure)
      {
         return Result.Failure<Guid>(result.Error);
      }

      eventRepository.Insert(result.Value);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return result.Value.Id;
   }
}
