﻿namespace EventModularMonolith.Modules.Events.Application.Venues.DTOs;
public sealed record VenueDto(Guid VenueId, string Name, string Description)
{

   public AddressDto Address { get; internal set;}
   public IReadOnlyCollection<string> ImageUrls { get; internal set; } = [];
}

public record AddressDto(string StreetAndNumber, string City, string Region, string Country, double Longitude, double Latitude);
