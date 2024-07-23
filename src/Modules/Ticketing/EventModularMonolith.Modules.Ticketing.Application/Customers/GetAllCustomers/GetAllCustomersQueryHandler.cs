// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data.Common;
using Dapper;
using EventModularMonolith.Modules.Ticketing.Application.Customers.DTOs;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Ticketing.Application.Customers.GetAllCustomers;

public sealed class GetAllCustomersQueryHandler(IDbConnectionFactory dbConnectionFactory) :
     IQueryHandler<GetAllCustomersQuery, IReadOnlyCollection<CustomerDto>>
{
   public async Task<Result<IReadOnlyCollection<CustomerDto>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
   {
      await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

      const string sql =
          $"""
             SELECT
                email AS {nameof(CustomerDto.Email)}, 
                first_name AS {nameof(CustomerDto.FirstName)}, 
                last_name AS {nameof(CustomerDto.LastName)},
             FROM ticketing.customers
             """;

      List<CustomerDto> customers = (await connection.QueryAsync<CustomerDto>(sql, request)).AsList();

      return customers;
   }
}


