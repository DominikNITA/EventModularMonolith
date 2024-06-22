using EventModularMonolith.Shared.Application.Clock;

namespace EventModularMonolith.Shared.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
