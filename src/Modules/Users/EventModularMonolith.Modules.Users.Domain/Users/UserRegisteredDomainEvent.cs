using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Domain.Users;

public sealed class UserRegisteredDomainEvent(UserId userId) : DomainEvent
{
   public UserId UserId { get; init; } = userId;
}
