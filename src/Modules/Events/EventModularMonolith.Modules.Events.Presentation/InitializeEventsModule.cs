using EventModularMonolith.Modules.Events.Application.Categories.CreateCategory;
using EventModularMonolith.Modules.Events.Application.Events.CreateEvent;
using EventModularMonolith.Modules.Events.Application.Speakers.CreateSpeaker;
using EventModularMonolith.Modules.Events.Application.Speakers.DTOs;
using EventModularMonolith.Modules.Events.Application.Venues.CreateVenue;
using EventModularMonolith.Modules.Events.Application.Venues.DTOs;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Events.Presentation;

internal sealed class InitializeEventsModule : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPost("eventsModule/initialize", async (ISender sender) =>
         {
            //TODO: Refactor

            // Categories
            Result<Guid> businessCategory = await sender.Send(new CreateCategoryCommand(
               "Business"));

            await sender.Send(new CreateCategoryCommand(
               "IT"));

            await sender.Send(new CreateCategoryCommand(
               "Politics"));

            // Venues
            //Result<Guid> venue1 = await sender.Send(new CreateVenueCommand(
            //   "National Stadium", "Beautiful stadium next to the Vistula river.", new AddressDto("Rondo Waszyngtona 1", "Warsaw", "Mazowieckie", "Poland", 21.045556, 52.239444)));

            //await sender.Send(new CreateVenueCommand(
            //   "Grand Venue", "Lorem ipsum", new AddressDto("Something Streer 12", "Paris", "Ile-de-France", "France", 5.045556, 45.239444)));

            //// Speakers
            //Result<Guid> speaker1 = await sender.Send(new CreateSpeakerCommand(
            //   "Jack Greenhill", "IT contractor", [new SpeakerLinkDto("https://x.com")]));

            //Result<Guid> speaker2 = await sender.Send(new CreateSpeakerCommand(
            //   "Mike Krolowicz", "CTO Veolia", [new SpeakerLinkDto("https://x.com/mike")]));

            ////// Events
            //await sender.Send(new CreateEventCommand(
            //   Guid.NewGuid(),
            //   businessCategory.Value,
            //   "Example Event",
            //   "Some generic description with long epithets etc...",
            //   venue1.Value,
            //   DateTime.Now.AddDays(2).ToUniversalTime(),
            //   DateTime.Now.AddDays(2).AddHours(5).ToUniversalTime(),
            //   new List<Guid>() {speaker1.Value, speaker2.Value}
            //));

            // Ticket types

            return businessCategory.Match(Results.Ok<Guid>, ApiResults.Problem);
         })
         .AllowAnonymous()
         .WithTags(Tags.Events);
   }
}
