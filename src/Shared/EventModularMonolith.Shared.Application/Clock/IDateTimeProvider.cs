﻿namespace EventModularMonolith.Shared.Application.Clock;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
