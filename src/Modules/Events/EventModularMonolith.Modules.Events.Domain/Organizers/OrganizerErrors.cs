using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.Organizers;

public static class OrganizerErrors
{
   public static Error NotFound(Guid organizerId) =>
       Error.NotFound("Organizers.NotFound", $"The organizer with the identifier {organizerId} was not found");
}
