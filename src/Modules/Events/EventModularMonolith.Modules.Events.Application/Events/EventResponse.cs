using EventModularMonolith.Modules.Events.Application.Events.GetEvent;
using EventModularMonolith.Modules.Events.Application.Venues.DTOs;

namespace EventModularMonolith.Modules.Events.Application.Events;

public sealed record EventResponse(
   Guid Id,
   Guid CategoryId,
   string Title,
   string Description,
   DateTime StartsAtUtc,
   DateTime? EndsAtUtc)
{
   public List<TicketTypeResponse> TicketTypes { get; set; } = [];
   public VenueDto Venue { get; set; }
   public string BackgroundImage { get; set; }
};
