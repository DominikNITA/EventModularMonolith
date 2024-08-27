// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EventModularMonolith.Modules.Events.Domain.Categories;

public interface ICategoryRepository
{
   Task InsertAsync(Category category, CancellationToken cancellationToken = default);

   Task<Category> GetByIdAsync(CategoryId id, CancellationToken cancellationToken = default);
}
