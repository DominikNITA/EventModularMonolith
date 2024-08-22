using EventModularMonolith.Modules.Events.Application.Events.CreateEvent;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using static EventModularMonolith.Modules.Events.Presentation.Organizers.CreateEvent;

namespace EventModularMonolith.Modules.Events.Presentation.Organizers;

internal sealed class CreateEvent : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPost("organizer/{id}/events", async (Guid id, CreateEventRequest request, ISender sender) =>
         {
            var command = new CreateEventCommand(
               id,
               request.CategoryId,
               request.Title,
               request.Description,
               request.VenueId,
               request.StartsAtUtc,
               request.EndsAtUtc,
               request.SpeakersIds);

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
         })
      .WithTags(Tags.Events)
      .Produces<Result<Guid>>();
   }

   internal sealed class CreateEventRequest
   {
      public Guid CategoryId { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public Guid VenueId { get; set; }
      public DateTime StartsAtUtc { get; set; }
      public DateTime? EndsAtUtc { get; set; }
      public IEnumerable<Guid> SpeakersIds { get; set; }
   }
}
