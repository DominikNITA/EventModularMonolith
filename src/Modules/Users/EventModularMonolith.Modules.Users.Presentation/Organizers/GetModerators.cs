// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Users.Application.Organizers.GetAllOrganizers;
using EventModularMonolith.Modules.Users.Application.Organizers.DTOs;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using EventModularMonolith.Modules.Users.Application.Organizers.GetModerators;
using EventModularMonolith.Shared.Infrastructure.Authentication;

namespace EventModularMonolith.Modules.Users.Presentation.Organizers;

internal sealed class GetModerators : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapGet("organizers/{organizerId}/moderators", async (Guid organizerId, ClaimsPrincipal claims, ISender sender) =>
         {
            if (claims.GetUserOrganizerId() != organizerId)
            {
               return Results.Unauthorized();
            }

            Result<IReadOnlyCollection<ModeratorDto>> result = await sender.Send(new GetModeratorsQuery(claims.GetUserOrganizerId()));

            return result.Match(Results.Ok, ApiResults.Problem);
         })
      .WithTags(Tags.Organizers)
      .Produces<Result<IReadOnlyCollection<ModeratorDto>>>()
      .WithName("GetModerators");
   }
}
