using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Domain.Users;

public sealed class UserProfileUpdatedDomainEvent(UserId userId, string firstName, string lastName) : DomainEvent
{
   public UserId UserId { get; init; } = userId;

   public string FirstName { get; init; } = firstName;

   public string LastName { get; init; } = lastName;
}
