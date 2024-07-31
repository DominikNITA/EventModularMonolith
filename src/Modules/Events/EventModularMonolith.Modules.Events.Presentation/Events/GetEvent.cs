using EventModularMonolith.Modules.Events.Application.Events;
using EventModularMonolith.Modules.Events.Application.Events.GetEvent;
using EventModularMonolith.Shared.Application.Caching;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Events.Presentation.Events;

internal sealed class GetEvent : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapGet("events/{id}", async (Guid id, ISender sender, ICacheService cacheService) =>
         {
            string cacheKey = $"getEvent-{id}";
            EventResponse eventResponse = await cacheService.GetAsync<EventResponse>(cacheKey);

            if (eventResponse is not null)
            {
               return Results.Ok(eventResponse);
            }

            Result<EventResponse> result = await sender.Send(new GetEventQuery(id));

            if (result.IsSuccess)
            {
               await cacheService.SetAsync(cacheKey, result.Value);
            }

            return result.Match(Results.Ok, ApiResults.Problem);
         })
         .WithTags(Tags.Events)
         .Produces<Result<EventResponse>>()
         .WithName("GetEvent");
   }
}
