using EventModularMonolith.Modules.Users.Application.Users.GetUser;
using EventModularMonolith.Shared.Domain;
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
      app.MapGet("users/{id}/profile", async (Guid id, ISender sender) =>
         {
            Result<UserResponse> result = await sender.Send(new GetUserQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
         })
         .WithTags(Tags.Users);
   }
}
