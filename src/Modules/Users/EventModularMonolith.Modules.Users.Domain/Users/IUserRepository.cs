namespace EventModularMonolith.Modules.Users.Domain.Users;


public interface IUserRepository
{
   Task InsertAsync(User user, CancellationToken cancellationToken = default);

   Task<User> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);

   Task<User> GetByIdentityId(string identityId, CancellationToken cancellationToken = default);
   Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
