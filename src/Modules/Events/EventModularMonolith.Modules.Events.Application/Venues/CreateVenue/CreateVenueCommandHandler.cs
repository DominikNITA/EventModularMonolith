// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Domain.Venues;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Application.Storage;
using EventModularMonolith.Shared.Domain;


namespace EventModularMonolith.Modules.Events.Application.Venues.CreateVenue;

public class CreateVenueCommandHandler(
    IVenueRepository venueRepository,
    IUnitOfWork unitOfWork,
    IBlobService blobService
) : ICommandHandler<CreateVenueCommand, Guid>
{
   public async Task<Result<Guid>> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
   {
      Address address = new(
         request.Address.StreetAndNumber,
         request.Address.City,
         request.Address.Region,
         request.Address.Country,
         request.Address.Longitude,
         request.Address.Latitude
         );

      Result<Venue> venue = Venue.Create(
            request.Name,
            request.Description,
            address
      );

      if (venue.IsFailure)
      {
         return Result.Failure<Guid>(venue.Error);
      }

      await venueRepository.InsertAsync(venue.Value, cancellationToken);

      await blobService.MoveFilesFromTempToEntityContainer(request.ImageContainers, "venue", venue.Value.Id.Value, cancellationToken);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return venue.Value.Id.Value;
   }
}

