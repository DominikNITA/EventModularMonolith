using EventModularMonolith.Modules.Users.Application.Abstractions.Identity;
using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Users.Application.Users.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : ICommand<AuthTokenWithRefresh>;

