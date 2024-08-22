using Dapper;
using EventModularMonolith.Modules.Users.Domain.Organizers;
using EventModularMonolith.Modules.Users.Domain.Organizers.Events;
using EventModularMonolith.Modules.Users.IntegrationEvents;
using EventModularMonolith.Shared.Application.EventBus;
using EventModularMonolith.Shared.Application.Exceptions;
using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Users.Application.Organizers.CreateOrganizer;

internal sealed class CreateOrganizerDomainEventHandler(IEventBus bus, IOrganizerRepository organizerRepository)
    : DomainEventHandler<OrganizerCreatedDomainEvent>
{
   public override async Task Handle(
      OrganizerCreatedDomainEvent domainEvent,
       CancellationToken cancellationToken = default)
   {
      Organizer organizer = await organizerRepository.GetByIdAsync(domainEvent.OrganizerId, cancellationToken);

      if (organizer is null)
      {
         throw new GeneralException(nameof(CreateOrganizerDomainEventHandler));
      }

      await bus.PublishAsync(
          new OrganizerCreatedIntegrationEvent(
              domainEvent.Id,
              domainEvent.OccurredOnUtc,
              organizer.Id.Value,
              organizer.OwnerId.Value,
              organizer.Name,
              organizer.Description,
              organizer.Moderators.Select(m => m.UserId.Value).AsList()),
          cancellationToken);
   }
}
