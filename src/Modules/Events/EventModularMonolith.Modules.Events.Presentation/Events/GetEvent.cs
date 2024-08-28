using System.Reflection;
using EventModularMonolith.Modules.Events.Application.Events;
using EventModularMonolith.Modules.Events.Application.Events.GetEvent;
using EventModularMonolith.Shared.Application.Caching;
using EventModularMonolith.Shared.Application.Messaging;
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
      app.MapGet("events/{id}", async (Guid id, ISender sender, ICacheService cacheService) => await CacheHelper.QueryWithCache($"getEvent-{id}", cacheService, sender, new GetEventQuery(id)))
         .WithTags(Tags.Events)
         .Produces<Result<EventResponse>>()
         .WithName("GetEvent");
   }
}



