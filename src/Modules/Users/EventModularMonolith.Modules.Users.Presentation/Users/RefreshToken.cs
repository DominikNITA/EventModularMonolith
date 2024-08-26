using System.Security.Claims;
using EventModularMonolith.Modules.Users.Application.Abstractions.Identity;
using EventModularMonolith.Modules.Users.Application.Users.GetAuthTokens;
using EventModularMonolith.Modules.Users.Application.Users.GetUser;
using EventModularMonolith.Modules.Users.Application.Users.RefreshToken;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Infrastructure.Authentication;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Users.Presentation.Users;

internal sealed class RefreshToken : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPost("auth/refresh", async (RefreshTokenRequest request, ISender sender) =>
         {
            Result<AuthTokenWithRefresh> result = await sender.Send(new RefreshTokenCommand(request.RefreshToken));

            return result.Match(Results.Ok, ApiResults.Problem);
         })
         .AllowAnonymous()
         .Produces<Result<AuthTokenWithRefresh>>()
         .WithTags(Tags.Users)
         .WithName("RefreshToken");
   }
}

internal sealed class RefreshTokenRequest
{
   public string RefreshToken { get; init; }
}
