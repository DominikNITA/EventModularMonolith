// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Domain.Organizers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventModularMonolith.Modules.Events.Infrastructure.Organizers;

#nullable disable
public class OrganizerConfiguration : IEntityTypeConfiguration<Organizer>
{
   public void Configure(EntityTypeBuilder<Organizer> builder)
   {
      builder.Ignore(e => e.DomainEvents);

      builder.HasKey(x => x.Id);

      builder.OwnsMany<Moderator>("Moderators", y =>
      {
         y.WithOwner().HasForeignKey("OrganizerId");
         y.ToTable("moderators");
         y.Property<ModeratorId>("UserId");
         y.Property<OrganizerId>("OrganizerId");
         y.HasKey("UserId", "OrganizerId");

         y.OwnsOne<ModeratorRole>("Role", b =>
         {
            b.Property<string>(x => x.Value).HasColumnName("Role");
         });
      });
   }
}
