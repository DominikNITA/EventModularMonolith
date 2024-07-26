// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data.Common;
using Dapper;
using EventModularMonolith.Modules.Ticketing.Application.Events.DTOs;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Ticketing.Application.Events.GetAllEvents;

public sealed class GetAllEventsQueryHandler(IDbConnectionFactory dbConnectionFactory) :
     IQueryHandler<GetAllEventsQuery, IReadOnlyCollection<EventDto>>
{
   public async Task<Result<IReadOnlyCollection<EventDto>>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
   {
      await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

      const string sql =
          $"""
             SELECT
               customer_id AS {nameof(EventDto.CustomerId)}, 
               title AS {nameof(EventDto.Title)}, 
               description AS {nameof(EventDto.Description)}, 
               location AS {nameof(EventDto.Location)}, 
               starts_at_utc AS {nameof(EventDto.StartsAtUtc)}, 
               ends_at_utc AS {nameof(EventDto.EndsAtUtc)}, 
               canceled AS {nameof(EventDto.Canceled)},
             FROM ticketing.events
             """;

      List<EventDto> events = (await connection.QueryAsync<EventDto>(sql, request)).AsList();

      return events;
   }
}


