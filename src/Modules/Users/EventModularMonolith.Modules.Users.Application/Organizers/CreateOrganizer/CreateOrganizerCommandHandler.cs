// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Users.Application.Abstractions.Data;
using EventModularMonolith.Modules.Users.Domain.Organizers;
using EventModularMonolith.Modules.Users.Domain.Users;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;
using MediatR;


namespace EventModularMonolith.Modules.Users.Application.Organizers.CreateOrganizer;

public class CreateOrganizerCommandHandler(
    IOrganizerRepository organizerRepository,
    IUnitOfWork unitOfWork,
    ISender sender
) : ICommandHandler<CreateOrganizerCommand, Guid>
{
   public async Task<Result<Guid>> Handle(CreateOrganizerCommand request, CancellationToken cancellationToken)
   {
      Result<Guid> ownerId = await sender.Send(request.UserRegistration, cancellationToken);

      if (ownerId.IsFailure)
      {
         return Result.Failure<Guid>(ownerId.Error);
      }

      Result<Organizer> organizer = Organizer.Create(
           request.Name,
           request.Description,
           new UserId(ownerId.Value)
      );

      if (organizer.IsFailure)
      {
         return Result.Failure<Guid>(organizer.Error);
      }

      await organizerRepository.InsertAsync(organizer.Value, cancellationToken);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return organizer.Value.Id.Value;
   }
}

