// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Ticketing.Domain.Events;
using EventModularMonolith.Modules.Ticketing.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Ticketing.Infrastructure.Events;

internal sealed class EventRepository(TicketingDbContext context) : IEventRepository
{
   public async Task<Event> GetByIdAsync(EventId id, CancellationToken cancellationToken = default)
   {
      return await context.Events.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
   }

   public async Task InsertAsync(Event @event, CancellationToken cancellationToken = default)
   {
      await context.Set<Event>().AddAsync(@event, cancellationToken);
   }
}
