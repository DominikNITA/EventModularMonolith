using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Users.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;
