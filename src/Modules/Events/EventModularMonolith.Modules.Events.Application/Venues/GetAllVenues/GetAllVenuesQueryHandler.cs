// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data.Common;
using Dapper;
using EventModularMonolith.Modules.Events.Application.Venues.DTOs;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Application.Venues.GetAllVenues;

public sealed class GetAllVenuesQueryHandler(IDbConnectionFactory dbConnectionFactory) :
     IQueryHandler<GetAllVenuesQuery, IReadOnlyCollection<VenueDto>>
{
    public async Task<Result<IReadOnlyCollection<VenueDto>>> Handle(GetAllVenuesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                  name AS {nameof(VenueDto.Name)}, 
                  description AS {nameof(VenueDto.Description)},
             FROM events.venues
""";

        List<VenueDto> venues = (await connection.QueryAsync<VenueDto>(sql, request)).AsList();

        return venues;
    }
}


