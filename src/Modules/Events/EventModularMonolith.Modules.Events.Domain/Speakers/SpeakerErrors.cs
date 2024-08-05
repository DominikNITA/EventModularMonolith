using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.Speakers;

public static class SpeakerErrors
{
    public static Error NotFound(Guid speakerId) =>
        Error.NotFound("Speakers.NotFound", $"The speaker with the identifier {speakerId} was not found");

}
