// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Ticketing.Domain.TicketTypes;
using EventModularMonolith.Modules.Ticketing.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Database;

namespace EventModularMonolith.Modules.Ticketing.Infrastructure.TicketTypes;

public sealed class TicketTypeRepository(TicketingDbContext context) : Repository<TicketType>(context), ITicketTypeRepository
{
   public void InsertRange(IEnumerable<TicketType> ticketTypes)
   {
      context.TicketTypes.AddRange(ticketTypes);
   }
}
