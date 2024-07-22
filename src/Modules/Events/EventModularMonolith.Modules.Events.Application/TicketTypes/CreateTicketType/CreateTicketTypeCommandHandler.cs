// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Modules.Events.Domain.TicketTypes;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;


namespace EventModularMonolith.Modules.Events.Application.TicketTypes.CreateTicketType;

public class CreateTicketTypeCommandHandler(
    ITicketTypeRepository ticketTypeRepository,
    IEventRepository eventRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateTicketTypeCommand, Guid>
{
   public async Task<Result<Guid>> Handle(CreateTicketTypeCommand request, CancellationToken cancellationToken)
   {
      Event? @event = await eventRepository.GetAsync(request.EventId, cancellationToken);

      if (@event == null)
      {
         return Result.Failure<Guid>(EventErrors.NotFound(request.EventId));
      }

      Result<TicketType> result = TicketType.Create(@event, request.Name, request.Price, request.Currency, request.Quantity);

      if (result.IsFailure)
      {
         return Result.Failure<Guid>(result.Error);
      }

      ticketTypeRepository.Insert(result.Value);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return result.Value.Id;
   }
}

