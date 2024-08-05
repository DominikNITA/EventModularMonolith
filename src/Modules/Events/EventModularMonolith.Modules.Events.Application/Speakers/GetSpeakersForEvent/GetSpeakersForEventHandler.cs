// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data.Common;
using Dapper;
using EventModularMonolith.Modules.Events.Application.Speakers.DTOs;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Application.Speakers.GetAllSpeakers;

public sealed class GetSpeakersForEventHandler(IDbConnectionFactory dbConnectionFactory) :
     IQueryHandler<GetSpeakersForEventQuery, IReadOnlyCollection<SpeakerDto>>
{
    public async Task<Result<IReadOnlyCollection<SpeakerDto>>> Handle(GetSpeakersForEventQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                  s.id as {nameof(SpeakerDto.Id)},
                  s.name AS {nameof(SpeakerDto.Name)}, 
                  s.description AS {nameof(SpeakerDto.Description)},
                  '/img/speakers/speaker-1.jpg' as {nameof(SpeakerDto.ImageUrl)},
                  l.url as {nameof(SpeakerLinkDto.Url)}
             FROM events.events e
                  LEFT JOIN events.event_speaker es ON es.events_id = e.id
                  LEFT JOIN events.speakers s ON s.id = es.speakers_id
                  LEFT JOIN events.links l ON l.speaker_id = s.id
             WHERE e.id = @EventId
""";
        Dictionary<Guid, SpeakerDto> speakersDictionary = [];
      await connection.QueryAsync<SpeakerDto, SpeakerLinkDto?, SpeakerDto>(
         sql,
         (speaker, link) =>
         {
            if (speakersDictionary.TryGetValue(speaker.Id, out SpeakerDto? existingEvent))
            {
               speaker = existingEvent;
            }
            else
            {
               speakersDictionary.Add(speaker.Id, speaker);
            }

            if (link is not null)
            {
               speaker.Links.Add(link);
            }

            return speaker;
         },
         request,
         splitOn: $"{nameof(SpeakerLinkDto.Url)}");


      return speakersDictionary.Values.ToList();
    }
}


