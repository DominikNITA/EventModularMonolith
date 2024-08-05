using EventModularMonolith.Modules.Users.Application.Users.RegisterUser;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Users.Presentation;

internal sealed class InitializeUsersModule : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPost("usersModule/initialize", async (ISender sender) =>
         {
            Result<Guid> result = await sender.Send(new RegisterUserCommand(
               "user@domain.com",
               "12345",
               "Test",
               "User"));

            return result.Match(Results.Ok<Guid>, ApiResults.Problem);
         })
         .AllowAnonymous()
         .WithTags(Tags.Users);
   }
}
