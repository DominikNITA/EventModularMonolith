// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.Venues.Events;

public sealed class VenueCreatedDomainEvent(Guid venueId) : DomainEvent
{
    public Guid VenueId { get; init;} = venueId;
}

