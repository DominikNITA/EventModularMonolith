using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.Venues;

public static class VenueErrors
{
    public static Error NotFound(Guid venueId) =>
        Error.NotFound("Venues.NotFound", $"The venue with the identifier {venueId} was not found");

}
