// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.Venues.Events;

public sealed class VenueCreatedDomainEvent(VenueId venueId) : DomainEvent
{
    public VenueId VenueId { get; init;} = venueId;
}

