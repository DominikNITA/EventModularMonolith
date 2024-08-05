using System.Data.Common;
using Dapper;
using EventModularMonolith.Modules.Events.Application.Venues.DTOs;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Application.Events.GetEvents;

internal sealed class GetEventsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetEventsQuery, IReadOnlyCollection<EventResponse>>
{
    public async Task<Result<IReadOnlyCollection<EventResponse>>> Handle(
        GetEventsQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 e.id AS {nameof(EventResponse.Id)},
                 e.category_id AS {nameof(EventResponse.CategoryId)},
                 e.title AS {nameof(EventResponse.Title)},
                 e.description AS {nameof(EventResponse.Description)},
                 e.starts_at_utc AS {nameof(EventResponse.StartsAtUtc)},
                 e.ends_at_utc AS {nameof(EventResponse.EndsAtUtc)},
                 v.id AS {nameof(VenueDto.VenueId)},
                 v.name AS {nameof(VenueDto.Name)},
                 v.description AS {nameof(VenueDto.Description)},
                 v.address_street_and_number as {nameof(AddressDto.StreetAndNumber)},
                 v.address_city as {nameof(AddressDto.City)},
                 v.address_region as {nameof(AddressDto.Region)},
                 v.address_country as {nameof(AddressDto.Country)},
                 v.address_longitude as {nameof(AddressDto.Longitude)},
                 v.address_latitude as {nameof(AddressDto.Latitude)}    
             FROM events.events e
                 LEFT JOIN events.venues v ON e.venue_id = v.id
             """;

        Dictionary<Guid, EventResponse> eventsDictionary = [];
        await connection.QueryAsync<EventResponse, VenueDto, AddressDto, EventResponse>(
           sql,
           (@event, venue, address) =>
           {
              if (eventsDictionary.TryGetValue(@event.Id, out EventResponse? existingEvent))
              {
                 @event = existingEvent;
              }
              else
              {
                 eventsDictionary.Add(@event.Id, @event);
              }

              if (@event.Venue is null)
              {
                 @event = @event with { Venue = venue with { Address = address } };
                 eventsDictionary[@event.Id] = @event;
              }

              return @event;
           },
           request,
           splitOn: $"{nameof(VenueDto.VenueId)},{nameof(AddressDto.StreetAndNumber)}");

        return eventsDictionary.Values.ToList();
    }
}
