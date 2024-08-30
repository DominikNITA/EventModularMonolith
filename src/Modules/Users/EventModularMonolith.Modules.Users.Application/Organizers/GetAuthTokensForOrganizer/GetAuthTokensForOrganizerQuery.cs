using EventModularMonolith.Modules.Users.Application.Abstractions.Identity;
using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Users.Application.Organizers.GetAuthTokensForOrganizer;

public sealed record GetAuthTokensForOrganizerQuery(string Email, string Password) : IQuery<AuthTokenWithRefresh>;

