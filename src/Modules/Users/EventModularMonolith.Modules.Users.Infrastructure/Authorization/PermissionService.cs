using EventModularMonolith.Modules.Users.Application.Organizers.DTOs;
using EventModularMonolith.Modules.Users.Application.Organizers.GetOrganizerForModerator;
using System.Threading;
using EventModularMonolith.Modules.Users.Application.Users.GetUserPermissions;
using EventModularMonolith.Modules.Users.Domain.Users;
using EventModularMonolith.Shared.Application.Authorization;
using EventModularMonolith.Shared.Application.Caching;
using EventModularMonolith.Shared.Domain;
using MediatR;
using static MassTransit.ValidationResultExtensions;
using Result = EventModularMonolith.Shared.Domain.Result;

namespace EventModularMonolith.Modules.Users.Infrastructure.Authorization;

internal sealed class PermissionService(ISender sender, ICacheService cacheService) : IPermissionService
{
   public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId)
   {
      string cacheKey = $"permissions-{identityId}";
      PermissionsResponse response = await cacheService.GetAsync<PermissionsResponse>(cacheKey);

      if (response is not null)
      {
         return response;
      }

      Result<PermissionsResponse> permissionsResult = await sender.Send(new GetUserPermissionsQuery(identityId));

      if (permissionsResult.IsSuccess)
      {
         Result<OrganizerDto> organizerResult = await sender.Send(new GetOrganizerForModeratorQuery(permissionsResult.Value.UserId));

         if (organizerResult.IsSuccess)
         {
            permissionsResult = Result.Success(permissionsResult.Value with { OrganizerId = organizerResult.Value.Id });

         }
         await cacheService.SetAsync(cacheKey, permissionsResult.Value, null);
      }

      return permissionsResult;
   }
}
