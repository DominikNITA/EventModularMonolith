// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Ticketing.Domain.TicketTypes.Events;

public sealed class TicketTypeSoldOutDomainEvent(TicketTypeId ticketTypeId) : DomainEvent
{
    public TicketTypeId TicketTypeId { get; init;} = ticketTypeId;
}

