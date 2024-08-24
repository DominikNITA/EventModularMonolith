using System.Net.Http.Json;
using EventModularMonolith.Modules.Users.Application.Abstractions.Identity;
using Microsoft.Extensions.Options;

namespace EventModularMonolith.Modules.Users.Infrastructure.Identity;

internal sealed class KeyCloakPublicClient(HttpClient httpClient, IOptions<KeyCloakOptions>options)
{
   internal async Task<AuthTokenWithRefresh> GetAuthTokens(string email, string password, CancellationToken cancellationToken = default)
   {
      var authRequestParameters = new KeyValuePair<string, string>[]
      {
         new("client_id", options.Value.PublicClientId),
         new("username", email),
         new("password", password),
         new("scope", "email openid"),
         new("grant_type", "password")
      };

      using var authRequestContent = new FormUrlEncodedContent(authRequestParameters);

      using var authRequest = new HttpRequestMessage(HttpMethod.Post, new Uri(options.Value.TokenUrl));

      authRequest.Content = authRequestContent;

      HttpResponseMessage res = await httpClient.SendAsync(authRequest, cancellationToken);

      res.EnsureSuccessStatusCode();

      return await res.Content.ReadFromJsonAsync<AuthTokenWithRefresh>(cancellationToken);
   }
}
