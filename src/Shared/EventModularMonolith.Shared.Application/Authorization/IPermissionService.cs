using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Shared.Application.Authorization;

public interface IPermissionService
{
    Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId);
}
