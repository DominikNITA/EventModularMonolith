// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data.Common;
using Dapper;
using EventModularMonolith.Modules.Ticketing.Application.Customers.DTOs;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Modules.Ticketing.Domain.Customers;

namespace EventModularMonolith.Modules.Ticketing.Application.Customers.GetCustomer;

public sealed class GetCustomerQueryHandler(IDbConnectionFactory dbConnectionFactory) :
     IQueryHandler<GetCustomerQuery, CustomerDto>
{
    public async Task<Result<CustomerDto>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                  email AS {nameof(CustomerDto.Email)}, 
                  first_name AS {nameof(CustomerDto.FirstName)}, 
                  last_name AS {nameof(CustomerDto.LastName)},
             FROM ticketing.customers
             WHERE id = @CustomerId
             """;

        CustomerDto? customer = await connection.QuerySingleOrDefaultAsync<CustomerDto>(sql, request);

        if (customer is null)
        {
            return Result.Failure<CustomerDto>(CustomerErrors.NotFound(request.CustomerId));
        }

        return customer;
    }
}
