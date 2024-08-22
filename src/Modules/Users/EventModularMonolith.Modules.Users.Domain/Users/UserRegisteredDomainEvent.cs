using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Domain.Users;

public sealed class UserRegisteredDomainEvent(Guid userId) : DomainEvent
{
   public Guid UserId { get; init; } = userId;
}
