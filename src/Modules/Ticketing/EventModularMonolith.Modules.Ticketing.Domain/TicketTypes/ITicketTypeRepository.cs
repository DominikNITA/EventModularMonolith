// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EventModularMonolith.Modules.Ticketing.Domain.TicketTypes;


public interface ITicketTypeRepository
{
   void InsertRange(IEnumerable<TicketType> ticketTypes);

   Task InsertAsync(TicketType ticketType, CancellationToken cancellationToken = default);

   Task<TicketType> GetByIdAsync(TicketTypeId id, CancellationToken cancellationToken = default);
}
