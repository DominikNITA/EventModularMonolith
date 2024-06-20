using EventModularMonolith.Modules.Events.Application.Abstractions.Clock;
using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Application.Abstractions.Messaging;
using EventModularMonolith.Modules.Events.Domain.Abstractions;
using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Domain.Events;
using MediatR;

namespace EventModularMonolith.Modules.Events.Application.Events.CreateEvent;

internal sealed class CreateEventCommandHandler(
   IDateTimeProvider dateTimeProvider, 
   IEventRepository eventRepository,
   ICategoryRepository categoryRepository,
   IUnitOfWork unitOfWork) : ICommandHandler<CreateEventCommand, Guid>
{

   public async Task<Result<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
   {
      if (request.StartsAtUtc < dateTimeProvider.UtcNow)
      {
         return Result.Failure<Guid>(EventErrors.StartDateInPast);
      }

      Category? category = await categoryRepository.GetAsync(request.CategoryId, cancellationToken);

      if (category is null)
      {
         return Result.Failure<Guid>(CategoryErrors.NotFound(request.CategoryId));
      }

      Result<Event> result = Event.Create(
         category,
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
