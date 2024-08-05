// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.Speakers.GetSpeaker;
using EventModularMonolith.Modules.Events.Application.Speakers.DTOs;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Events.Presentation.Speakers;

internal sealed class GetSpeaker : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapGet("speakers/{id}", async (Guid id, ISender sender) =>
         {
            Result<SpeakerDto> result = await sender.Send(new GetSpeakerQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
         })
      .WithTags(Tags.Speakers)
      .Produces<Result<SpeakerDto>>()
      .WithName("GetSpeaker");
   }
}