using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Modules.Events.Domain.Organizers;
using EventModularMonolith.Modules.Events.Domain.Speakers;
using EventModularMonolith.Modules.Events.Domain.Venues;
using EventModularMonolith.Shared.Application.Clock;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Application.Storage;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Application.Events.CreateEvent;

internal sealed class CreateEventCommandHandler(
   IDateTimeProvider dateTimeProvider, 
   IEventRepository eventRepository,
   ICategoryRepository categoryRepository,
   ISpeakerRepository speakerRepository,
   IOrganizerRepository organizerRepository,
   IBlobService blobService,
   IUnitOfWork unitOfWork) : ICommandHandler<CreateEventCommand, Guid>
{

   public async Task<Result<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
   {
      if (request.StartsAtUtc < dateTimeProvider.UtcNow)
      {
         return Result.Failure<Guid>(EventErrors.StartDateInPast);
      }

      Category? category = await categoryRepository.GetByIdAsync(new CategoryId(request.CategoryId), cancellationToken);

      if (category is null)
      {
         return Result.Failure<Guid>(CategoryErrors.NotFound(request.CategoryId));
      }

      IEnumerable<Speaker> speakers = speakerRepository.GetSpeakersByIds(request.SpeakerIds.Select(x => new SpeakerId(x)));

      Organizer organizer = await organizerRepository.GetByIdAsync(new OrganizerId(request.OrganizerId), cancellationToken);

      if (organizer is null)
      {
         return Result.Failure<Guid>(OrganizerErrors.NotFound(request.OrganizerId));
      }

      Result<Event> result = organizer.CreateEvent(
         category,
         request.Title,
         request.Description,
         new VenueId(request.VenueId),
         request.StartsAtUtc,
         request.EndsAtUtc,
         speakers.ToList());

      if (result.IsFailure)
      {
         return Result.Failure<Guid>(result.Error);
      }

      await eventRepository.InsertAsync(result.Value, cancellationToken);

      // TODO: Do it through events/notifications - might crash
      await blobService.MoveFilesFromTempToEntityContainer(request.ImageContainers, "event", result.Value.Id.Value, cancellationToken);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return result.Value.Id.Value;
   }
}
