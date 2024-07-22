// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data.Common;
using Dapper;
using EventModularMonolith.Modules.Events.Application.Events.GetEvent;
using EventModularMonolith.Modules.Events.Application.TicketTypes.DTOs;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Modules.Events.Domain.TicketTypes;

namespace EventModularMonolith.Modules.Events.Application.TicketTypes.GetTicketType;

public sealed class GetTicketTypeQueryHandler(IDbConnectionFactory dbConnectionFactory) :
     IQueryHandler<GetTicketTypeQuery, TicketTypeDto>
{
    public async Task<Result<TicketTypeDto>> Handle(GetTicketTypeQuery request, CancellationToken cancellationToken)
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
              WHERE id = @TicketTypeId
              """;

        TicketTypeDto? ticketType = await connection.QuerySingleOrDefaultAsync<TicketTypeDto>(sql, request);

        if (ticketType is null)
        {
            return Result.Failure<TicketTypeDto>(TicketTypeErrors.NotFound(request.TicketTypeId));
        }

        return ticketType;
    }
}
