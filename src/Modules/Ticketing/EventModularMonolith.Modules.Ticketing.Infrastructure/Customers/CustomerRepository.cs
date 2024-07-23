// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Ticketing.Domain.Customers;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Ticketing.Infrastructure.Customers;

public interface ICustomerRepository : IRepository<Customer>
{

}
