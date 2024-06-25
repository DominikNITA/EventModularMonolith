using EventModularMonolith.Modules.Users.Domain.Users;
using EventModularMonolith.Modules.Users.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Database;

namespace EventModularMonolith.Modules.Users.Infrastructure.Users;

internal sealed class UserRepository(UsersDbContext context) : Repository<User>(context), IUserRepository
{
}
