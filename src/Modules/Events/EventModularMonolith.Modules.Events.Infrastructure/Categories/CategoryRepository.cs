﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Database;

namespace EventModularMonolith.Modules.Events.Infrastructure.Categories;

internal sealed class CategoryRepository(EventsDbContext context) : Repository<Category>(context), ICategoryRepository
{

}
