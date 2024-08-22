// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Events.Application.Organizers.CreateOrganizer;

public sealed record CreateOrganizerCommand(
    Guid OrganizerId,
    string Name,
    string Description,
    Guid OwnerId
) : ICommand<Guid>;

