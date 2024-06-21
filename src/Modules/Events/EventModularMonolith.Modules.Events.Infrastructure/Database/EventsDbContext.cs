using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Modules.Events.Domain.TicketTypes;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Events.Infrastructure.Database;

public sealed class EventsDbContext : DbContext, IUnitOfWork
{
   public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options) { }

   internal DbSet<Event> Events { get; set; }
   internal DbSet<Category> Categories { get; set; }
   internal DbSet<TicketType> TicketTypes { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.HasDefaultSchema(Schemas.Events);
   }
}
