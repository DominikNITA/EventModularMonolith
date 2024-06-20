using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Events.Infrastructure.Database;

public sealed class EventsDbContext : DbContext, IUnitOfWork
{
   public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options) { }

   internal DbSet<Event> Events { get; set; }
   internal DbSet<Category> Categories { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.HasDefaultSchema(Schemas.Events);
   }
}
