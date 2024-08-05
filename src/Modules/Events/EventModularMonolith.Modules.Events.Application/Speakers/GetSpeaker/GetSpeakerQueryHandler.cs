// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data.Common;
using Dapper;
using EventModularMonolith.Modules.Events.Application.Speakers.DTOs;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Modules.Events.Domain.Speakers;

namespace EventModularMonolith.Modules.Events.Application.Speakers.GetSpeaker;

public sealed class GetSpeakerQueryHandler(IDbConnectionFactory dbConnectionFactory) :
     IQueryHandler<GetSpeakerQuery, SpeakerDto>
{
    public async Task<Result<SpeakerDto>> Handle(GetSpeakerQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                     name AS {nameof(SpeakerDto.Name)}, 
    description AS {nameof(SpeakerDto.Description)},
             FROM events.speakers
             WHERE id = @SpeakerId
""";

        SpeakerDto? speaker = await connection.QuerySingleOrDefaultAsync<SpeakerDto>(sql, request);

        if (speaker is null)
        {
            return Result.Failure<SpeakerDto>(SpeakerErrors.NotFound(request.SpeakerId));
        }

        return speaker;
    }
}
