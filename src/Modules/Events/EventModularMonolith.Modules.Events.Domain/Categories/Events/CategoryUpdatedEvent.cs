// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.Categories.Events;

public sealed class CategoryUpdatedDomainEvent(CategoryId categoryId, string name) : DomainEvent
{
    public CategoryId CategoryId { get; init;} = categoryId;
    public string Name { get; init; } = name;
}

