// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data.Common;
using Dapper;
using EventModularMonolith.Modules.Events.Application.Venues.DTOs;
using EventModularMonolith.Modules.Events.Application.Venues.GetAllVenues;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Application.Organizers.GetVenuesForOrganizer;

public sealed class GetVenuesForOrganizerQueryHandler(IDbConnectionFactory dbConnectionFactory) :
     IQueryHandler<GetVenuesForOrganizerQuery, IReadOnlyCollection<VenueGridDto>>
{
    public async Task<Result<IReadOnlyCollection<VenueGridDto>>> Handle(GetVenuesForOrganizerQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                  id AS {nameof(VenueGridDto.VenueId)},
                  name AS {nameof(VenueGridDto.Name)}, 
                  description AS {nameof(VenueGridDto.Description)},
                  concat_ws(', ', address_street_and_number, address_city) as {nameof(VenueGridDto.ShortAddress)}
             FROM events.venues
             WHERE organizer_id = @OrganizerId 
""";

        List<VenueGridDto> venues = (await connection.QueryAsync<VenueGridDto>(sql, request)).AsList();

        return venues;
    }
}


