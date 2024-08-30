using EventModularMonolith.Modules.Users.Application.Abstractions.Identity;
using EventModularMonolith.Modules.Users.Application.Organizers.GetAuthTokensForOrganizer;
using EventModularMonolith.Modules.Users.Application.Users.GetAuthTokens;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Users.Presentation.Organizers;

internal sealed class GetAuthTokens : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPost("auth/organizer/login", async (GetAuthTokensRequest request, ISender sender) =>
         {
            Result<AuthTokenWithRefresh> result = await sender.Send(new GetAuthTokensForOrganizerQuery(request.Email, request.Password));

            return result.Match(Results.Ok, ApiResults.Problem);
         })
         .AllowAnonymous()
         .Produces<Result<AuthTokenWithRefresh>>()
         .WithTags(Tags.Users)
         .WithName("GetAuthTokensForOrganizer");
   }
}

internal sealed class GetAuthTokensRequest
{
   public string Email { get; init; }

   public string Password { get; init; }
}
