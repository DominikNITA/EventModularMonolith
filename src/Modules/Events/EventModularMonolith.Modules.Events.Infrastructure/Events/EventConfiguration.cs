using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Modules.Events.Domain.Organizers;
using EventModularMonolith.Modules.Events.Domain.Venues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventModularMonolith.Modules.Events.Infrastructure.Events;

internal sealed class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasOne<Category>().WithMany().HasForeignKey(e => e.CategoryId);
        builder.HasOne<Venue>().WithMany().HasForeignKey(e => e.VenueId);
        builder.HasOne<Organizer>().WithMany().HasForeignKey(e => e.OrganizerId);
        builder.HasMany(e => e.Speakers).WithMany(e => e.Events);
        builder.Property(e => e.Id)
           .HasConversion(id => id.Value, value => new EventId(value));
   }
}
