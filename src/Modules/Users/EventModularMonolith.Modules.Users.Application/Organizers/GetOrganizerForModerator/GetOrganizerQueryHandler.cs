// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data.Common;
using Dapper;
using EventModularMonolith.Modules.Users.Application.Organizers.DTOs;
using EventModularMonolith.Modules.Users.Application.Organizers.GetOrganizer;
using EventModularMonolith.Modules.Users.Domain.Organizers;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Application.Organizers.GetOrganizerForModerator;

public sealed class GetOrganizerForModeratorHandler(IDbConnectionFactory dbConnectionFactory) :
     IQueryHandler<GetOrganizerForModeratorQuery, OrganizerDto>
{
    public async Task<Result<OrganizerDto>> Handle(GetOrganizerForModeratorQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
                SELECT
                     o.id AS {nameof(OrganizerDto.Id)},
                     o.name AS {nameof(OrganizerDto.Name)}, 
                     o.description AS {nameof(OrganizerDto.Description)}
                FROM users.organizers o
                JOIN users.moderators m ON m.organizer_id = o.id
                WHERE m.user_id = @UserId
            """;

        OrganizerDto? organizer = await connection.QuerySingleOrDefaultAsync<OrganizerDto>(sql, request);

        if (organizer is null)
        {
            return Result.Failure<OrganizerDto>(OrganizerErrors.NotFound(Guid.Empty));
        }

        return organizer;
    }
}
