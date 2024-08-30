using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Shared.Application.Authorization;

public interface IOrganizerService
{
    Task<Result<Guid>> GetUserOrganizer(Guid UserId, CancellationToken cancellationToken = default);
}
