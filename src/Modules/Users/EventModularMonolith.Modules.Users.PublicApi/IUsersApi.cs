namespace EventModularMonolith.Modules.Users.PublicApi;

public interface IUsersApi
{
   Task<UserResponse?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
