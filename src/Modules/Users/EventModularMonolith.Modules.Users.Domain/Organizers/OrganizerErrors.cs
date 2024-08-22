using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Domain.Organizers;

public static class OrganizerErrors
{
   public static Error NotFound(Guid organizerId) =>
       Error.NotFound("Organizers.NotFound", $"The organizer with the identifier {organizerId} was not found");

   public static Error OrganizerNotUnverified() =>
       Error.Problem("Organizers.OrganizerNotUnverified", "The organizer is not unverified");

   public static Error UserIsAlreadyAModerator() =>
       Error.Problem("Organizers.UserIsAlreadyAModerator", $"User is already an moderator");
}
