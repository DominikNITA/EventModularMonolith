// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Domain.Organizers;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Application.Organizers.CreateOrganizer;

public class CreateOrganizerCommandHandler(
    IOrganizerRepository organizerRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateOrganizerCommand, Guid>
{
   public async Task<Result<Guid>> Handle(CreateOrganizerCommand request, CancellationToken cancellationToken)
   {
      Result<Organizer> result = Organizer.Create(
         request.OrganizerId,
         request.Name,
         request.Description,
        new ModeratorId(request.OwnerId)
      );

      if (result.IsFailure)
      {
         return Result.Failure<Guid>(result.Error);
      }

      await organizerRepository.InsertAsync(result.Value, cancellationToken);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return result.Value.Id.Value;
   }
}

