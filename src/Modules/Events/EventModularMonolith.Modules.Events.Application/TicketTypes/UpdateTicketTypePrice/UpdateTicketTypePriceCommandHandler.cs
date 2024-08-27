// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Domain.TicketTypes;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;

public class UpdateTicketTypePriceCommandHandler(
    ITicketTypeRepository ticketTypeRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<UpdateTicketTypePriceCommand>
{
   public async Task<Result> Handle(UpdateTicketTypePriceCommand request, CancellationToken cancellationToken)
   {
      TicketType? ticketType = await ticketTypeRepository.GetByIdAsync(new TicketTypeId(request.TicketTypeId), cancellationToken);

      if (ticketType is null)
      {
         return Result.Failure(TicketTypeErrors.NotFound(request.TicketTypeId));
      }

      ticketType.UpdatePrice(request.Price);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return Result.Success();
   }
}

