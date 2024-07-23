// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Ticketing.Domain.Customers.Events;

public sealed class CustomerDeletedDomainEvent(Guid customerId) : DomainEvent
{
    public Guid CustomerId { get; init;} = customerId;
}