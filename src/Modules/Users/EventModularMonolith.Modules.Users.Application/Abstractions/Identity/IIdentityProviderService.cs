using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Application.Abstractions.Identity;

public interface IIdentityProviderService
{
   Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default);
   Task<Result<AuthTokenWithRefresh>> GetAuthTokens(string email, string password, CancellationToken cancellationToken = default);
   Task<Result<AuthTokenWithRefresh>> RefreshToken(string refreshToken, CancellationToken cancellationToken = default);
}
