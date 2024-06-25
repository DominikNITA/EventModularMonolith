using MediatR;

namespace EventModularMonolith.Shared.Domain;

public interface IDomainEvent : INotification
{
   Guid Id { get; }
   DateTime OccurredOnUtc { get; }
}
