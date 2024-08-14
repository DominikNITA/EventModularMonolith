using System.Data.Common;
using Dapper;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Modules.Events.Application.Venues.DTOs;
using EventModularMonolith.Shared.Application.Storage;

namespace EventModularMonolith.Modules.Events.Application.Events.GetEvent;

internal sealed class GetEventQueryHandler(IDbConnectionFactory dbConnectionFactory, IBlobService blobService) : IQueryHandler<GetEventQuery, EventResponse>
{
   public async Task<Result<EventResponse>> Handle(GetEventQuery request, CancellationToken cancellationToken)
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
              v.address_latitude as {nameof(AddressDto.Latitude)},                                                 
              tt.id AS {nameof(TicketTypeResponse.TicketTypeId)},
              tt.name AS {nameof(TicketTypeResponse.Name)},
              tt.price AS {nameof(TicketTypeResponse.Price)},
              tt.currency AS {nameof(TicketTypeResponse.Currency)},
              tt.quantity AS {nameof(TicketTypeResponse.Quantity)}
          FROM events.events e
          LEFT JOIN events.ticket_types tt ON tt.event_id = e.id
          LEFT JOIN events.venues v ON e.venue_id = v.id
          WHERE e.id = @EventId
          """;

      Dictionary<Guid, EventResponse> eventsDictionary = [];
      await connection.QueryAsync<EventResponse, VenueDto, AddressDto, TicketTypeResponse?, EventResponse>(
         sql,
         (@event, venue, address, ticketType) =>
         {
            if (eventsDictionary.TryGetValue(@event.Id, out EventResponse? existingEvent))
            {
               @event = existingEvent;
            }
            else
            {
               eventsDictionary.Add(@event.Id, @event);
            }

            if (ticketType is not null)
            {
               @event.TicketTypes.Add(ticketType);
            }

            if(@event.Venue is null)
            {
               @event = @event with { Venue = venue with { Address = address } };
               eventsDictionary[@event.Id] = @event;
            }

            return @event;
         },
         request,
         splitOn: $"{nameof(VenueDto.VenueId)},{nameof(AddressDto.StreetAndNumber)}, {nameof(TicketTypeResponse.TicketTypeId)}");

      if (!eventsDictionary.TryGetValue(request.EventId, out EventResponse eventResponse))
      {
         return Result.Failure<EventResponse>(EventErrors.NotFound(request.EventId));
      }

      IReadOnlyCollection<string> eventImages = await blobService.GetUrlsFromContainerAsync("event", eventResponse.Id, cancellationToken);
      eventResponse.BackgroundImage = eventImages.FirstOrDefault() ?? "";

      IReadOnlyCollection<string> venueImages = await blobService.GetUrlsFromContainerAsync("venue", eventResponse.Venue.VenueId, cancellationToken);
      eventResponse.Venue.ImageUrls = venueImages;

      return eventResponse;
   }
}
