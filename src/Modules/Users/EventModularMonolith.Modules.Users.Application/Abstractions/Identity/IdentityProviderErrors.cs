using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Application.Abstractions.Identity;

public static class IdentityProviderErrors
{
   public static readonly Error EmailIsNotUnique = Error.Conflict(
      "Identity.EmailIsNotUnique",
      "The specified email is not unique.");

   public static readonly Error InvalidCredentials = Error.Conflict(
      "Identity.InvalidCredentials",
      "Invalid password.");
}
