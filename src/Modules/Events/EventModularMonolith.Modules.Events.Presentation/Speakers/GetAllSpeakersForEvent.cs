// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.Speakers.DTOs;
using EventModularMonolith.Modules.Events.Application.Speakers.GetSpeakersForEvent;
using EventModularMonolith.Shared.Application.Caching;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Events.Presentation.Speakers;

internal sealed class GetAllSpeakersForEvent : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapGet("events/{id}/speakers", async (Guid id,ISender sender, ICacheService cacheService) =>  await CacheHelper.QueryWithCache($"getEvent-{id}", cacheService, sender, new GetSpeakersForEventQuery(id)))
      .WithTags(Tags.Speakers)
      .Produces<Result<IReadOnlyCollection<SpeakerDto>>>()
      .WithName("GetSpeakersForEvent");
   }
}
