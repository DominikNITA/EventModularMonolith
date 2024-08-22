// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Domain.Organizers.Events;

public sealed class OrganizerDeletedDomainEvent(Guid organizerId) : DomainEvent
{
    public Guid OrganizerId { get; init;} = organizerId;
}