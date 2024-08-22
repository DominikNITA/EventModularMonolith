// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Domain.Organizers.Events;

public sealed class OrganizerVerifiedDomainEvent(OrganizerId organizerId) : DomainEvent
{
    public OrganizerId OrganizerId { get; init;} = organizerId;
}

