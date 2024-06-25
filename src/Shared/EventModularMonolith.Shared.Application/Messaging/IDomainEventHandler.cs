using EventModularMonolith.Shared.Domain;
using MediatR;

namespace EventModularMonolith.Shared.Application.Messaging;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
   where TDomainEvent : IDomainEvent;
