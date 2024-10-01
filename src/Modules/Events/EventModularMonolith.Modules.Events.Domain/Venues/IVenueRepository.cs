// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EventModularMonolith.Modules.Events.Domain.Venues;

public interface IVenueRepository
{
   Task InsertAsync(Venue venue, CancellationToken cancellationToken = default);

   Task<Venue?> GetByIdAsync(VenueId id, CancellationToken cancellationToken = default);
}
