using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventModularMonolith.Modules.Users.Application.Users.GetUser;
using EventModularMonolith.Modules.Users.PublicApi;
using EventModularMonolith.Shared.Domain;
using MediatR;
using UserResponse = EventModularMonolith.Modules.Users.PublicApi.UserResponse;

namespace EventModularMonolith.Modules.Users.Infrastructure.PublicApi;
internal sealed class UsersApi(ISender sender) : IUsersApi
{
   public async Task<UserResponse?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default)
   {
      Result<Application.Users.GetUser.UserResponse> userResult = await sender.Send(new GetUserQuery(userId), cancellationToken);

      if (userResult.IsFailure)
      {
         return null;
      }

      return new(userResult.Value.Id, userResult.Value.Email, userResult.Value.FirstName, userResult.Value.LastName);
   }
}
