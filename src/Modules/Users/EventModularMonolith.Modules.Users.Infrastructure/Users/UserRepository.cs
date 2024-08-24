using EventModularMonolith.Modules.Users.Domain.Users;
using EventModularMonolith.Modules.Users.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Users.Infrastructure.Users;


internal sealed class UserRepository(UsersDbContext context) : IUserRepository
{
   public async Task<User> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
   {
      return await context.Users.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
   }

   public async Task InsertAsync(User user, CancellationToken cancellationToken = default)
   {
      foreach (Role role in user.Roles)
      {
         context.Attach(role);
      }

      await context.Set<User>().AddAsync(user, cancellationToken);
   }
}
