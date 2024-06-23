﻿using EventModularMonolith.Modules.Events.Application.Events.CreateEvent;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Events.Presentation.Events;

internal static class CreateEvent
{
   public static void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPost("events", async (Request request, ISender sender) =>
         {
            var command = new CreateEventCommand(
               request.CategoryId,
               request.Title,
               request.Description,
               request.Location,
               request.StartsAtUtc,
               request.EndsAtUtc);

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
         })
      .WithTags(Tags.Events);
   }

   internal sealed class Request
   {
      public Guid CategoryId { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public string Location { get; set; }
      public DateTime StartsAtUtc { get; set; }
      public DateTime? EndsAtUtc { get; set; }
   }
}
