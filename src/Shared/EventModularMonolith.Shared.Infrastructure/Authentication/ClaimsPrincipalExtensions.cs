using System.Security.Claims;
using EventModularMonolith.Shared.Application.Exceptions;

namespace EventModularMonolith.Shared.Infrastructure.Authentication;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirst(CustomClaims.Sub)?.Value;

        return Guid.TryParse(userId, out Guid parsedUserId) ?
            parsedUserId :
            throw new GeneralException("User identifier is unavailable");
    }

    public static string GetIdentityId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
               throw new GeneralException("User identity is unavailable");
    }

    public static Guid GetUserOrganizerId(this ClaimsPrincipal? principal)
    {
       string? organizerId = principal?.FindFirst(CustomClaims.OrganizerId)?.Value;

       return Guid.TryParse(organizerId, out Guid parsedOrganizerId) ?
          parsedOrganizerId :
          throw new GeneralException("User identifier is unavailable");
   }

   public static HashSet<string> GetPermissions(this ClaimsPrincipal? principal)
    {
        IEnumerable<Claim> permissionClaims = principal?.FindAll(CustomClaims.Permission) ??
                                              throw new GeneralException("Permissions are unavailable");

        return permissionClaims.Select(c => c.Value).ToHashSet();
    }
}
