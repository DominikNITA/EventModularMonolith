// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.Speakers.CreateSpeaker;
using EventModularMonolith.Modules.Events.Application.Speakers.DTOs;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Events.Presentation.Speakers;

internal sealed class CreateSpeaker : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPost("speakers", async (CreateSpeakerRequest request, ISender sender) =>
         {
            var command = new CreateSpeakerCommand(
                    request.Name,
                    request.Description,
                    request.Links
            );

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
         })
      .WithTags(Tags.Speakers)
      .Produces<Result<Guid>>();
   }

   internal sealed class CreateSpeakerRequest
   {
      public string Name { get; set; } = string.Empty;
      public string Description { get; set; } = string.Empty;
      public List<SpeakerLinkDto> Links { get; set; } = [];
   }
}
