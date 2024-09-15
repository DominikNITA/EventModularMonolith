// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Users.Application.Organizers.CreateOrganizer;
using EventModularMonolith.Modules.Users.Application.Users.RegisterUser;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Users.Presentation.Organizers;

internal sealed class CreateOrganizer : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPost("organizers", async (CreateOrganizerRequest request, ISender sender) =>
         {
            var command = new CreateOrganizerCommand(
                    request.Name,
                    request.Description,
                    request.RegisterUserCommand
            );

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
         })
      .WithTags(Tags.Organizers)
      .Produces<Result<Guid>>()
      .WithName("CreateOrganizer");
   }

   internal sealed class CreateOrganizerRequest
   {
      public string Name { get; set; } = string.Empty;
      public string Description { get; set; } = string.Empty;
      public RegisterUserCommand RegisterUserCommand { get; set; }
   }
}
