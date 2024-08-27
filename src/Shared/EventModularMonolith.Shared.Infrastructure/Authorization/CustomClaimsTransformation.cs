using System.Security.Claims;
using EventModularMonolith.Shared.Application.Authorization;
using EventModularMonolith.Shared.Application.Exceptions;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace EventModularMonolith.Shared.Infrastructure.Authorization;

internal sealed class CustomClaimsTransformation(IServiceScopeFactory serviceScopeFactory) : IClaimsTransformation
{
   public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
   {
      if (principal.HasClaim(c => c.Type == CustomClaims.Sub))
      {
         return principal;
      }

      if (principal.Identity?.IsAuthenticated == false)
      {
         return principal;
      }

      using IServiceScope scope = serviceScopeFactory.CreateScope();

      IPermissionService permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

      string identityId = principal.GetIdentityId();

      Result<PermissionsResponse> result = await permissionService.GetUserPermissionsAsync(identityId);

      if (result.IsFailure)
      {
         throw new GeneralException(nameof(IPermissionService.GetUserPermissionsAsync), result.Error);
      }

      var claimsIdentity = new ClaimsIdentity();

      claimsIdentity.AddClaim(new Claim(CustomClaims.Sub, result.Value.UserId.ToString()));

      if (result.Value.Permissions is not null)
      {
         foreach (string permission in result.Value.Permissions)
         {
            claimsIdentity.AddClaim(new Claim(CustomClaims.Permission, permission));
         }
      }

      principal.AddIdentity(claimsIdentity);

      return principal;
   }
}

