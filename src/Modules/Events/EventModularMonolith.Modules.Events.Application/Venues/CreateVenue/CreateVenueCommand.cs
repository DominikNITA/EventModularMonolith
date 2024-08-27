// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.Venues.DTOs;
using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Events.Application.Venues.CreateVenue;

public sealed record CreateVenueCommand(
   string Name,
   string Description,
   AddressDto Address
) : ICommand<Guid>
{
   public IEnumerable<string> ImageContainers { get; set; }
}; 

