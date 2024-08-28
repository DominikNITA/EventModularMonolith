using EventModularMonolith.Modules.Events.Application.Events;
using EventModularMonolith.Modules.Events.Application.Events.GetEvents;
using EventModularMonolith.Shared.Application.Caching;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Events.Presentation.Events;

internal sealed class GetEvents : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {

      app.MapGet("events", async (ISender sender, ICacheService cacheService) => await CacheHelper.QueryWithCache($"getEvents", cacheService, sender, new GetEventsQuery()))
      .WithTags(Tags.Events)
      .Produces<Result<IReadOnlyCollection<EventResponse>>>()
      .WithName("GetEvents");
   }
}
