using System.Security.Claims;
using EventModularMonolith.Modules.Users.Application.Users.GetUser;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Infrastructure.Authentication;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Users.Presentation.Users;

internal sealed class GetUserProfile : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapGet("users/profile", async (ClaimsPrincipal claims, ISender sender) =>
         {
            Result<UserResponse> result = await sender.Send(new GetUserQuery(claims.GetUserId()));

            return result.Match(Results.Ok, ApiResults.Problem);
         })
         .RequireAuthorization()
         .WithTags(Tags.Users);
   }
}
