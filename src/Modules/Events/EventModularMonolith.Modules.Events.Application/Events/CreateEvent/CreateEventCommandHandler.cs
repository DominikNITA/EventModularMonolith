using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Modules.Events.Domain.Speakers;
using EventModularMonolith.Shared.Application.Clock;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Application.Events.CreateEvent;

internal sealed class CreateEventCommandHandler(
   IDateTimeProvider dateTimeProvider, 
   IEventRepository eventRepository,
   ICategoryRepository categoryRepository,
   ISpeakerRepository speakerRepository,
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

      IEnumerable<Speaker> speakers = speakerRepository.GetSpeakersByIds(request.SpeakerIds);

      Result<Event> result = Event.Create(
         category,
         request.Title,
         request.Description,
         request.VenueId,
         request.StartsAtUtc,
         request.EndsAtUtc,
         speakers.ToList());

      if (result.IsFailure)
      {
         return Result.Failure<Guid>(result.Error);
      }

      eventRepository.Insert(result.Value);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return result.Value.Id;
   }
}
