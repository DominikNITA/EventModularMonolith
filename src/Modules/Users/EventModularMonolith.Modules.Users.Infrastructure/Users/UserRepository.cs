using EventModularMonolith.Modules.Users.Domain.Users;
using EventModularMonolith.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;

namespace EventModularMonolith.Modules.Users.Infrastructure.Users;


internal sealed class UserRepository(UsersDbContext context) : IUserRepository
{
   public async Task<User> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
   {
      return await context.Users.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
   }

   public async Task<User> GetByIdentityId(string identityId, CancellationToken cancellationToken = default)
   {
      return await context.Users.SingleOrDefaultAsync(u => u.IdentityId == identityId, cancellationToken);
   }

   public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
   {
      return await context.Users.SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
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
