using EventModularMonolith.Modules.Users.Application.Users.UpdateUser;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Users.Presentation.Users;

internal sealed class UpdateUserProfile : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPut("users/{id}/profile", async (Guid id, UpdateUserProfileRequest request, ISender sender) =>
         {
            Result result = await sender.Send(new UpdateUserCommand(
               id,
               request.FirstName,
               request.LastName));

            return result.Match(Results.NoContent, ApiResults.Problem);
         })
         .WithTags(Tags.Users);
   }

   internal sealed class UpdateUserProfileRequest
   {
      public string FirstName { get; init; }

      public string LastName { get; init; }
   }
}