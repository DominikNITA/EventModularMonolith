using System.Net;
using EventModularMonolith.Modules.Users.Application.Abstractions.Identity;
using EventModularMonolith.Shared.Domain;
using Microsoft.Extensions.Logging;

namespace EventModularMonolith.Modules.Users.Infrastructure.Identity;

internal sealed class IdentityProviderService(KeyCloakAdminClient keyCloakAdminClient, KeyCloakPublicClient keyCloakPublicClient, ILogger<IdentityProviderService> logger) : IIdentityProviderService
{
   private const string PasswordCredentialType = "Password";

   public async Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default)
   {
      var userRepresentation = new UserRepresentation(
         user.Email,
         user.Email,
         user.FirstName,
         user.LastName,
         true,
         true,
         [new CredentialRepresentation(PasswordCredentialType, user.Password, false)]
      );

      try
      {
         string identityId = await keyCloakAdminClient.RegisterUserAsync(userRepresentation, cancellationToken);
         return identityId;
      }
      catch (HttpRequestException exception) when (exception.StatusCode == HttpStatusCode.Conflict)
      {
         logger.LogError(exception, "User registration failed");
         
         return Result.Failure<string>(IdentityProviderErrors.EmailIsNotUnique);
      }
   }

   public async Task<Result<AuthTokenWithRefresh>> GetAuthTokens(string email, string password, CancellationToken cancellationToken = default)
   {
      try
      {
         AuthTokenWithRefresh tokens = await keyCloakPublicClient.GetAuthTokens(email, password, cancellationToken);
         return tokens;
      }
      catch (HttpRequestException exception)
      {
         logger.LogError(exception, "Auth token retrieval failed");

         return Result.Failure<AuthTokenWithRefresh>(IdentityProviderErrors.InvalidCredentials);
      }
   }
}
