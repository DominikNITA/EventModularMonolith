using EventModularMonolith.Modules.Events.Application.Abstractions.Clock;

namespace EventModularMonolith.Modules.Events.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
