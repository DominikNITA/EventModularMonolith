using System.Data.Common;
using Dapper;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Events.Application.Events.GetEvent;

internal sealed class GetEventQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetEventQuery, EventResponse>
{
   public async Task<Result<EventResponse>> Handle(GetEventQuery request, CancellationToken cancellationToken)
   {
      await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

      const string sql =
         $"""
          SELECT
              e.id AS {nameof(EventResponse.Id)},
              e.title AS {nameof(EventResponse.Title)},
              e.description AS {nameof(EventResponse.Description)},
              e.location AS {nameof(EventResponse.Location)},
              e.starts_at_utc AS {nameof(EventResponse.StartsAtUtc)},
              e.ends_at_utc AS {nameof(EventResponse.EndsAtUtc)}
          FROM events.events e
          WHERE e.id = @EventId
          """;

      EventResponse? @event = await connection.QuerySingleOrDefaultAsync<EventResponse>(sql, request);

      if (@event == null)
      {
         return Result.Failure<EventResponse>(EventErrors.NotFound(request.EventId));
      }

      return @event;
   }
}
