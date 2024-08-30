// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data.Common;
using Dapper;
using EventModularMonolith.Modules.Users.Application.Organizers.DTOs;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Modules.Users.Domain.Organizers;

namespace EventModularMonolith.Modules.Users.Application.Organizers.GetOrganizer;

public sealed class GetOrganizerQueryHandler(IDbConnectionFactory dbConnectionFactory) :
     IQueryHandler<GetOrganizerQuery, OrganizerDto>
{
    public async Task<Result<OrganizerDto>> Handle(GetOrganizerQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
               id AS {nameof(OrganizerDto.Id)},
               name AS {nameof(OrganizerDto.Name)}, 
               description AS {nameof(OrganizerDto.Description)},
             FROM users.organizers
             WHERE id = @OrganizerId
""";

        OrganizerDto? organizer = await connection.QuerySingleOrDefaultAsync<OrganizerDto>(sql, request);

        if (organizer is null)
        {
            return Result.Failure<OrganizerDto>(OrganizerErrors.NotFound(request.OrganizerId));
        }

        return organizer;
    }
}
