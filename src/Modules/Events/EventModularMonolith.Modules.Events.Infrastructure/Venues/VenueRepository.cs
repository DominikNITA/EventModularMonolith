// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Domain.Venues;
using EventModularMonolith.Modules.Events.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Events.Infrastructure.Venues;


internal sealed class VenueRepository(EventsDbContext context) : IVenueRepository
{
   public async Task<Venue> GetByIdAsync(VenueId id, CancellationToken cancellationToken = default)
   {
      return await context.Venues.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
   }

   public async Task InsertAsync(Venue venue, CancellationToken cancellationToken = default)
   {
      await context.Set<Venue>().AddAsync(venue, cancellationToken);
   }
}
