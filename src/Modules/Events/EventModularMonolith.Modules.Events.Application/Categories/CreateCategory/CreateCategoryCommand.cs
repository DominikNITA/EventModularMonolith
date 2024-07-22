// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Events.Application.Categories.CreateCategory;

public sealed record CreateCategoryCommand(
    string Name
): ICommand<Guid>; 

