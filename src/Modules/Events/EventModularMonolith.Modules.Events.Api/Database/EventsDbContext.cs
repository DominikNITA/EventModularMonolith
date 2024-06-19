using EventModularMonolith.Modules.Events.Api.Events;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Events.Api.Database;

public sealed class EventsDbContext : DbContext
{
   public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options) { }

   internal DbSet<Event> Events { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.HasDefaultSchema(Schemas.Events);
   }
}
