using EventModularMonolith.Modules.Users.Application.Organizers.DTOs;
using EventModularMonolith.Modules.Users.Application.Organizers.GetOrganizerForModerator;
using EventModularMonolith.Modules.Users.Application.Users.GetUserPermissions;
using EventModularMonolith.Shared.Application.Authorization;
using EventModularMonolith.Shared.Application.Caching;
using EventModularMonolith.Shared.Domain;
using MediatR;
using static Dapper.SqlMapper;

namespace EventModularMonolith.Modules.Users.Infrastructure.Authorization;

internal sealed class OrganizerService(ISender sender, ICacheService cacheService) : IOrganizerService
{
   public async Task<Result<Guid>> GetUserOrganizer(Guid UserId, CancellationToken cancellationToken = default)
   {
      string cacheKey = $"user-organizer-{UserId}";
      Guid? response = await cacheService.GetAsync<Guid?>(cacheKey, cancellationToken);

      if (response is not null)
      {
         return response;
      }

      Result<OrganizerDto> result = await sender.Send(new GetOrganizerForModeratorQuery(UserId), cancellationToken);

      if (result.IsSuccess)
      {
         await cacheService.SetAsync(cacheKey, result.Value.Id, null, cancellationToken);
      }

      return result.Value.Id;
   }
}
