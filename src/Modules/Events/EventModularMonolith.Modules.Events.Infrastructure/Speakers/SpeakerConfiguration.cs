// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Domain.Speakers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventModularMonolith.Modules.Events.Infrastructure.Speakers;

#nullable disable
public class SpeakerConfiguration : IEntityTypeConfiguration<Speaker>
{
    public void Configure(EntityTypeBuilder<Speaker> builder)
    {
        builder.Ignore(e => e.DomainEvents);
        builder.HasMany(s => s.Links).WithOne().HasForeignKey(l => l.SpeakerId);
        builder.Property(e => e.Id)
           .HasConversion(id => id.Value, value => new SpeakerId(value));
   }
}

public class LinkConfiguration : IEntityTypeConfiguration<Link>
{
   public void Configure(EntityTypeBuilder<Link> builder)
   {
      builder.Property(l => l.Url)
         .HasConversion(v => v.ToString(), v => new Uri(v));

      builder.HasKey(l => new { l.Url, l.SpeakerId});
   }
}
