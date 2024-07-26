// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Ticketing.Domain.Customers;
using EventModularMonolith.Modules.Ticketing.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Database;

namespace EventModularMonolith.Modules.Ticketing.Infrastructure.Customers;

public sealed class CustomerRepository(TicketingDbContext context) : Repository<Customer>(context), ICustomerRepository
{

}
