// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EventModularMonolith.Modules.Ticketing.Domain.Customers;

public interface ICustomerRepository
{
   Task InsertAsync(Customer customer, CancellationToken cancellationToken = default);

   Task<Customer> GetByIdAsync(CustomerId id, CancellationToken cancellationToken = default);
}
