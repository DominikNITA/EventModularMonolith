// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Ticketing.Domain.TicketTypes;
using EventModularMonolith.Modules.Ticketing.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Ticketing.Infrastructure.TicketTypes;

internal sealed class TicketTypeRepository(TicketingDbContext context) : ITicketTypeRepository
{
   public async Task<TicketType> GetByIdAsync(TicketTypeId id, CancellationToken cancellationToken = default)
   {
      return await context.TicketTypes.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
   }

   public async Task InsertAsync(TicketType ticketType, CancellationToken cancellationToken = default)
   {
      await context.Set<TicketType>().AddAsync(ticketType, cancellationToken);
   }

   public void InsertRange(IEnumerable<TicketType> ticketTypes)
   {
      context.TicketTypes.AddRange(ticketTypes);
   }
}
