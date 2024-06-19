﻿namespace EventModularMonolith.Modules.Events.Domain.Abstractions;

public interface IDomainEvent
{
   Guid Id { get; }
   DateTime OccuredOnUtc { get; }
}
