using EventModularMonolith.Modules.Users.Application.Users.RegisterUser;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Users.Presentation.Users;

internal sealed class RegisterUser : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPost("users/register", async (RegisterUserRequest request, ISender sender) =>
         {
            Result<Guid> result = await sender.Send(new RegisterUserCommand(
               request.Email,
               request.Password,
               request.FirstName,
               request.LastName));

            return result.Match(Results.Ok<Guid>, ApiResults.Problem);
         })
         .AllowAnonymous()
         .WithTags(Tags.Users);
   }

   internal sealed class RegisterUserRequest
   {
      public string Email { get; init; }

      public string Password { get; init; }

      public string FirstName { get; init; }

      public string LastName { get; init; }
   }
}