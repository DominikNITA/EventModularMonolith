namespace EventModularMonolith.Shared.Application.Authorization;

public sealed record PermissionsResponse(Guid UserId, HashSet<string> Permissions, Guid? OrganizerId);
