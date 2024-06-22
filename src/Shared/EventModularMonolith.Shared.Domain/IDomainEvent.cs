﻿namespace EventModularMonolith.Shared.Domain;

public interface IDomainEvent
{
   Guid Id { get; }
   DateTime OccurredOnUtc { get; }
}
