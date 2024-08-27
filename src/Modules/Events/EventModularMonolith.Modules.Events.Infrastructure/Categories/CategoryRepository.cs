// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Events.Infrastructure.Categories;

internal sealed class CategoryRepository(EventsDbContext context) : ICategoryRepository
{
   public async Task<Category> GetByIdAsync(CategoryId id, CancellationToken cancellationToken = default)
   {
      return await context.Categories.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
   }

   public async Task InsertAsync(Category category, CancellationToken cancellationToken = default)
   {
      await context.Set<Category>().AddAsync(category, cancellationToken);
   }
}
