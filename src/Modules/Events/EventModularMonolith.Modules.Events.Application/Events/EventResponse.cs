using EventModularMonolith.Modules.Events.Application.Events.GetEvent;

namespace EventModularMonolith.Modules.Events.Application.Events;

public sealed record EventResponse(
   Guid Id,
   Guid CategoryId,
   string Title,
   string Description,
   string Location,
   DateTime StartsAtUtc,
   DateTime? EndsAtUtc)
{
   public List<TicketTypeResponse> TicketTypes { get; } = [];
};
