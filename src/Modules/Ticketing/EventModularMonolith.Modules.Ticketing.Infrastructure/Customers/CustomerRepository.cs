// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Ticketing.Domain.Customers;
using EventModularMonolith.Modules.Ticketing.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Ticketing.Infrastructure.Customers;

internal sealed class CustomerRepository(TicketingDbContext context) : ICustomerRepository
{
   public async Task<Customer> GetByIdAsync(CustomerId id, CancellationToken cancellationToken = default)
   {
      return await context.Customers.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
   }

   public async Task InsertAsync(Customer customer, CancellationToken cancellationToken = default)
   {
      await context.Set<Customer>().AddAsync(customer, cancellationToken);
   }
}
