// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Ticketing.Application.Abstractions.Data;
using EventModularMonolith.Modules.Ticketing.Domain.Events;
using EventModularMonolith.Modules.Ticketing.Domain.TicketTypes;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;


namespace EventModularMonolith.Modules.Ticketing.Application.Events.CreateEvent;

public class CreateEventCommandHandler(
    IEventRepository eventRepository,
    ITicketTypeRepository ticketTypeRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateEventCommand>
{
   public async Task<Result> Handle(CreateEventCommand request, CancellationToken cancellationToken)
   {
      Result<Event> @event = Event.Create(
           request.EventId,
           request.Title,
           request.Description,
           request.Location,
           request.StartsAtUtc,
           request.EndsAtUtc
      );

      await eventRepository.InsertAsync(@event.Value, cancellationToken);

      IEnumerable<TicketType> ticketTypes = request.TicketTypes
         .Select(t => TicketType.Create(t.TicketTypeId, new EventId(t.EventId), t.Name, t.Price, t.Currency, t.Quantity));

      ticketTypeRepository.InsertRange(ticketTypes);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return Result.Success();
   }
}

