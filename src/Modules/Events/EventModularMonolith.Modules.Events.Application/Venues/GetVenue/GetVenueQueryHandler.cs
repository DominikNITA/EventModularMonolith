// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data.Common;
using Dapper;
using EventModularMonolith.Modules.Events.Application.Venues.DTOs;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Modules.Events.Domain.Venues;
using EventModularMonolith.Shared.Application.Storage;

namespace EventModularMonolith.Modules.Events.Application.Venues.GetVenue;

public sealed class GetVenueQueryHandler(IDbConnectionFactory dbConnectionFactory, IBlobService blobService) :
     IQueryHandler<GetVenueQuery, VenueDto>
{
   public async Task<Result<VenueDto>> Handle(GetVenueQuery request, CancellationToken cancellationToken)
   {
      await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

      const string sql =
          $"""
             SELECT
                  v.id AS {nameof(VenueDto.VenueId)},
                  v.name AS {nameof(VenueDto.Name)},
                  v.description AS {nameof(VenueDto.Description)},
                  v.address_street_and_number as {nameof(AddressDto.StreetAndNumber)},
                  v.address_city as {nameof(AddressDto.City)},
                  v.address_region as {nameof(AddressDto.Region)},
                  v.address_country as {nameof(AddressDto.Country)},
                  v.address_longitude as {nameof(AddressDto.Longitude)},
                  v.address_latitude as {nameof(AddressDto.Latitude)}
             FROM events.venues v
             WHERE id = @VenueId
""";
      Dictionary<Guid, VenueDto> venuesDictionary = [];

      await connection.QueryAsync<VenueDto, AddressDto, VenueDto>(
           sql,
           (venue, address) =>
           {
              venue.Address = address;
              venuesDictionary.Add(venue.VenueId, venue);
              return venue;
           },
           request,
           splitOn:
           $"{nameof(AddressDto.StreetAndNumber)}");

      VenueDto? venue = venuesDictionary.GetValueOrDefault(request.VenueId);

      if (venue is null)
      {
         return Result.Failure<VenueDto>(VenueErrors.NotFound(request.VenueId));
      }

      venue.ImageUrls = await blobService.GetUrlsFromContainerAsync("venue", venue.VenueId, cancellationToken);

      return venue;
   }
}
