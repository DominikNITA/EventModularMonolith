// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Users.Domain.Users;

namespace EventModularMonolith.Modules.Users.Domain.Organizers;

public interface IOrganizerRepository
{
   Task InsertAsync(Organizer organizer, CancellationToken cancellationToken = default);

   Task<Organizer> GetByIdAsync(OrganizerId id, CancellationToken cancellationToken = default);

   Task<bool> IsUserModeratingAnyOrganizerAsync(UserId  userId, CancellationToken cancellationToken = default);
}
