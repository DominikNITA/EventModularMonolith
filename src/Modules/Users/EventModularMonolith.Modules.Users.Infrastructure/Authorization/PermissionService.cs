using EventModularMonolith.Modules.Users.Application.Users.GetUserPermissions;
using EventModularMonolith.Shared.Application.Authorization;
using EventModularMonolith.Shared.Domain;
using MediatR;

namespace EventModularMonolith.Modules.Users.Infrastructure.Authorization;

internal sealed class PermissionService(ISender sender) : IPermissionService
{
   public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId)
   {
      return await sender.Send(new GetUserPermissionsQuery(identityId));
   }
}
