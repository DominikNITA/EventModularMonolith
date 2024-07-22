// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data.Common;
using Dapper;
using EventModularMonolith.Modules.Events.Application.TicketTypes.DTOs;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Application.TicketTypes.GetAllTicketTypes;

public sealed class GetAllTicketTypesQueryHandler(IDbConnectionFactory dbConnectionFactory) :
     IQueryHandler<GetAllTicketTypesQuery, IReadOnlyCollection<TicketTypeDto>>
{
    public async Task<Result<IReadOnlyCollection<TicketTypeDto>>> Handle(GetAllTicketTypesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(TicketTypeDto.Id)},
                 event_id AS {nameof(TicketTypeDto.EventId)},
                 name AS {nameof(TicketTypeDto.Name)},
                 price AS {nameof(TicketTypeDto.Price)},
                 currency AS {nameof(TicketTypeDto.Currency)},
                 quantity AS {nameof(TicketTypeDto.Quantity)}
             FROM events.ticket_types
             """;

        List<TicketTypeDto> ticketTypes = (await connection.QueryAsync<TicketTypeDto>(sql, request)).AsList();

        return ticketTypes;
    }
}


