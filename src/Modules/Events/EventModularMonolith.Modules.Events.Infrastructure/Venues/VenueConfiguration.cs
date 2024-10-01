// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Domain.Organizers;
using EventModularMonolith.Modules.Events.Domain.Venues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventModularMonolith.Modules.Events.Infrastructure.Venues;

#nullable disable
public class VenueConfiguration : IEntityTypeConfiguration<Venue>
{
   public void Configure(EntityTypeBuilder<Venue> builder)
   {
      builder.Ignore(e => e.DomainEvents);

      builder.OwnsOne(x => x.Address);
      builder.Property(e => e.Id)
         .HasConversion(id => id.Value, value => new VenueId(value));
      builder.HasOne<Organizer>().WithMany().HasForeignKey(e => e.OrganizerId);
   }
}
