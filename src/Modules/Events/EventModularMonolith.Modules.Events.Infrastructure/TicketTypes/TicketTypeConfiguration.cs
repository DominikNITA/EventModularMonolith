using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Modules.Events.Domain.TicketTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventModularMonolith.Modules.Events.Infrastructure.TicketTypes;

internal sealed class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
{
   public void Configure(EntityTypeBuilder<TicketType> builder)
   {
      builder.HasOne<Event>().WithMany().HasForeignKey(t => t.EventId);
      builder.Property(e => e.Id)
         .HasConversion(id => id.Value, value => new TicketTypeId(value));
   }
}
