// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Users.Application.Organizers.GetOrganizer;
using EventModularMonolith.Modules.Users.Application.Organizers.DTOs;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Users.Presentation.Organizers;

internal sealed class GetOrganizer : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapGet("organizers/{id}", async (Guid id, ISender sender) =>
         {
            Result<OrganizerDto> result = await sender.Send(new GetOrganizerQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
         })
      .WithTags(Tags.Organizers)
      .Produces<Result<OrganizerDto>>()
      .WithName("GetOrganizer");
   }
}