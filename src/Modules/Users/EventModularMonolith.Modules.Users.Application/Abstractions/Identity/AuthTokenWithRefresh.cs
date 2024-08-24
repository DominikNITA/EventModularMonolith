using System.Text.Json.Serialization;

namespace EventModularMonolith.Modules.Users.Application.Abstractions.Identity;

public sealed class AuthTokenWithRefresh
{
   [JsonPropertyName("access_token")]
   public string AccessToken { get; init; }
   [JsonPropertyName("refresh_token")]
   public string RefreshToken { get; init; }
}
