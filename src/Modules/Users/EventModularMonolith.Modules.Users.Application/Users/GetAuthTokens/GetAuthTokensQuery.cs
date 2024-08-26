using EventModularMonolith.Modules.Users.Application.Abstractions.Identity;
using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Users.Application.Users.GetAuthTokens;

public sealed record GetAuthTokensQuery(string Email, string Password) : IQuery<AuthTokenWithRefresh>;

