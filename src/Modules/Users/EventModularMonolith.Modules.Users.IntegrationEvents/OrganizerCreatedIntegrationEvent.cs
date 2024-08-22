using EventModularMonolith.Shared.Application.EventBus;

namespace EventModularMonolith.Modules.Users.IntegrationEvents;

public sealed class OrganizerCreatedIntegrationEvent : IntegrationEvent
{
   public OrganizerCreatedIntegrationEvent(
       Guid id,
       DateTime occurredOnUtc,
       Guid organizerId,
       Guid ownerId,
       string name,
       string description, 
       IReadOnlyCollection<Guid> activeModerators)
       : base(id, occurredOnUtc)
   {
      OrganizerId = organizerId;
      OwnerId = ownerId;
      Name = name;
      Description = description;
      ActiveModerators = activeModerators;
   }

   public Guid OrganizerId { get; }
   public Guid OwnerId { get; init; }
   public string Name { get; init; }
   public string Description { get; init; }
   public IReadOnlyCollection<Guid> ActiveModerators { get; set; }
}
