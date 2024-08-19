// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventModularMonolith.Modules.Events.Infrastructure.Categories;

#nullable disable
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Ignore(e => e.DomainEvents);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
           .HasConversion(id => id.Value, value => new CategoryId(value));
   }
}
