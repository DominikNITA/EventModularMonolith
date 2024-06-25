using EventModularMonolith.Modules.Users.Application.Abstractions.Data;
using EventModularMonolith.Modules.Users.Domain.Users;
using EventModularMonolith.Modules.Users.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Users.Infrastructure.Database;

public sealed class UsersDbContext : DbContext, IUnitOfWork
{
   public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

   internal DbSet<User> Users { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.HasDefaultSchema(Schemas.Users);

      modelBuilder.ApplyConfiguration(new UserConfiguration());
   }
}
