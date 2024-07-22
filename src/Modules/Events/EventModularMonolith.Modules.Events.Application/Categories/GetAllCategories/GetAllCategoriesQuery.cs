// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.Categories.DTOs;
using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Events.Application.Categories.GetAllCategories;

public sealed record GetAllCategoriesQuery : IQuery<IReadOnlyCollection<CategoryDto>>;