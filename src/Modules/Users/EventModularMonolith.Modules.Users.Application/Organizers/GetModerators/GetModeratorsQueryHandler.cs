// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data.Common;
using Dapper;
using EventModularMonolith.Modules.Users.Application.Organizers.DTOs;
using EventModularMonolith.Modules.Users.Domain.Organizers;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Application.Organizers.GetModerators;

public sealed class GetModeratorsQueryHandler(IDbConnectionFactory dbConnectionFactory) :
     IQueryHandler<GetModeratorsQuery, IReadOnlyCollection<ModeratorDto>>
{
    public async Task<Result<IReadOnlyCollection<ModeratorDto>>> Handle(GetModeratorsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string ownerRole = "Owner";
        const string sql =
            $"""
             SELECT
                  u.id AS {nameof(ModeratorDto.UserId)},
                  u.first_name AS {nameof(ModeratorDto.FirstName)}, 
                  u.last_name AS {nameof(ModeratorDto.LastName)},
                  u.email AS {nameof(ModeratorDto.Email)},
                  m.is_active AS {nameof(ModeratorDto.IsActive)},
                  m.Role = '{ownerRole}' AS {nameof(ModeratorDto.IsOwner)}
             FROM users.moderators m
             JOIN users.users u ON m.user_id = u.id 
             WHERE organizer_id = @OrganizerId
            """;

        List<ModeratorDto> moderators = (await connection.QueryAsync<ModeratorDto>(sql, request)).AsList();

        return moderators;
    }
}


