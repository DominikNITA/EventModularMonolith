using EventModularMonolith.Modules.Events.Application.Organizers.CreateOrganizer;
using EventModularMonolith.Modules.Users.IntegrationEvents;
using EventModularMonolith.Shared.Application.EventBus;
using EventModularMonolith.Shared.Application.Exceptions;
using EventModularMonolith.Shared.Domain;
using MediatR;

namespace EventModularMonolith.Modules.Events.Presentation.Organizers;

internal sealed class OrganizerCreatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<OrganizerCreatedIntegrationEvent>
{
   public override async Task Handle(
      OrganizerCreatedIntegrationEvent integrationEvent,
       CancellationToken cancellationToken = default)
   {
      Result result = await sender.Send(
          new CreateOrganizerCommand(
              integrationEvent.OrganizerId,
              integrationEvent.Name,
              integrationEvent.Description,
              integrationEvent.OwnerId),
          cancellationToken);

      if (result.IsFailure)
      {
         throw new GeneralException(nameof(CreateOrganizerCommand), result.Error);
      }
   }
}
