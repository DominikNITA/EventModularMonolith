using EventModularMonolith.Modules.Users.Application.Abstractions.Identity;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Users.Application.Users.GetAuthTokens;

internal sealed class GetAuthTokensQueryHandler(
   IIdentityProviderService identityProviderService
)
   : IQueryHandler<GetAuthTokensQuery, AuthTokenWithRefresh>
{
   public async Task<Result<AuthTokenWithRefresh>> Handle(GetAuthTokensQuery request, CancellationToken cancellationToken)
   {
      Result<AuthTokenWithRefresh> result =
         await identityProviderService.GetAuthTokens(
            request.Email,
            request.Password,
            cancellationToken);

      if (result.IsFailure)
      {
         return Result.Failure<AuthTokenWithRefresh>(result.Error);
      }

      return result.Value;
   }
}
