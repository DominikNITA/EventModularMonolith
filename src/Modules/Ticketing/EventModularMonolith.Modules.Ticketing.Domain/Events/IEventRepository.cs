// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EventModularMonolith.Modules.Ticketing.Domain.Events;

public interface IEventRepository
{
   Task InsertAsync(Event @event, CancellationToken cancellationToken = default);

   Task<Event> GetByIdAsync(EventId id, CancellationToken cancellationToken = default);
}
