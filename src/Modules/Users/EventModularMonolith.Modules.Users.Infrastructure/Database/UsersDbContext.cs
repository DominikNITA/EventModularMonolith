﻿using EventModularMonolith.Modules.Users.Application.Abstractions.Data;
using EventModularMonolith.Modules.Users.Domain.Organizers;
using EventModularMonolith.Modules.Users.Domain.Users;
using EventModularMonolith.Modules.Users.Infrastructure.Organizers;
using EventModularMonolith.Modules.Users.Infrastructure.Users;
using EventModularMonolith.Shared.Infrastructure.Inbox;
using EventModularMonolith.Shared.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Users.Infrastructure.Database;

public sealed class UsersDbContext : DbContext, IUnitOfWork
{
   public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

   internal DbSet<User> Users { get; set; }

   internal DbSet<Organizer> Organizers { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.HasDefaultSchema(Schemas.Users);

      modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
      modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
      modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
      modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
      modelBuilder.ApplyConfiguration(new UserConfiguration());
      modelBuilder.ApplyConfiguration(new RoleConfiguration());
      modelBuilder.ApplyConfiguration(new PermissionConfiguration());
      modelBuilder.ApplyConfiguration(new OrganizerConfiguration());
   }
}
