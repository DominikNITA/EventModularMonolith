using EventModularMonolith.Modules.Users.Application.Abstractions.Identity;
using EventModularMonolith.Modules.Users.Application.Users.GetAuthTokens;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Application.Users.RefreshToken;

internal sealed class RefreshTokenCommandHandler(
   IIdentityProviderService identityProviderService
)
   : ICommandHandler<RefreshTokenCommand, AuthTokenWithRefresh>
{
   public async Task<Result<AuthTokenWithRefresh>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
   {
      Result<AuthTokenWithRefresh> result =
         await identityProviderService.RefreshToken(
            request.RefreshToken,
            cancellationToken);

      if (result.IsFailure)
      {
         return Result.Failure<AuthTokenWithRefresh>(result.Error);
      }

      return result.Value;
   }
}
