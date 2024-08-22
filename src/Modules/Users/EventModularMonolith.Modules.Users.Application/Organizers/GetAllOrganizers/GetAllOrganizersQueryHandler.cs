// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data.Common;
using Dapper;
using EventModularMonolith.Modules.Users.Application.Organizers.DTOs;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Application.Organizers.GetAllOrganizers;

public sealed class GetAllOrganizersQueryHandler(IDbConnectionFactory dbConnectionFactory) :
     IQueryHandler<GetAllOrganizersQuery, IReadOnlyCollection<OrganizerDto>>
{
    public async Task<Result<IReadOnlyCollection<OrganizerDto>>> Handle(GetAllOrganizersQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                     name AS {nameof(OrganizerDto.Name)}, 
    description AS {nameof(OrganizerDto.Description)},
             FROM users.organizers
""";

        List<OrganizerDto> organizers = (await connection.QueryAsync<OrganizerDto>(sql, request)).AsList();

        return organizers;
    }
}


