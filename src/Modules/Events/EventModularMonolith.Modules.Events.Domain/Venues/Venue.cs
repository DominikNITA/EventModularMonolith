﻿using EventModularMonolith.Modules.Events.Domain.Venues.Events;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.Venues;

public sealed class Venue : Entity
{
   private Venue() { }
   public VenueId Id { get; set; }
   public string Name { get; private set; }
   public string Description { get; private set; }
   public Address Address { get; private set; }

   public static Result<Venue> Create(string name, string description, Address address)
   {
      var venue = new Venue()
      {
         Id = new VenueId(Guid.NewGuid()),
         Name = name,
         Description = description,
         Address = address,
      };

      venue.Raise(new VenueCreatedDomainEvent(venue.Id));

      return venue;
   }
}


public class VenueId(Guid value) : TypedIdValueBase(value) { }
