using EventModularMonolith.Modules.Ticketing.Application.Abstractions.Data;
using EventModularMonolith.Modules.Ticketing.Domain.Customers;
using EventModularMonolith.Modules.Ticketing.Domain.Events;
using EventModularMonolith.Modules.Ticketing.Domain.TicketTypes;
using EventModularMonolith.Modules.Ticketing.Infrastructure.Customers;
using EventModularMonolith.Modules.Ticketing.Infrastructure.Events;
using EventModularMonolith.Modules.Ticketing.Infrastructure.TicketTypes;
using EventModularMonolith.Shared.Infrastructure.Inbox;
using EventModularMonolith.Shared.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Ticketing.Infrastructure.Database;

public sealed class TicketingDbContext : DbContext, IUnitOfWork
{
   public TicketingDbContext(DbContextOptions<TicketingDbContext> options) : base(options) { }

   internal DbSet<Event> Events { get; set; }
   internal DbSet<Customer> Customers { get; set; }
   internal DbSet<TicketType> TicketTypes { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.HasDefaultSchema(Schemas.Ticketing);

      modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
      modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
      modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
      modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
      modelBuilder.ApplyConfiguration(new EventConfiguration());
      modelBuilder.ApplyConfiguration(new TicketTypeConfiguration());
      modelBuilder.ApplyConfiguration(new CustomerConfiguration());
   }
}
