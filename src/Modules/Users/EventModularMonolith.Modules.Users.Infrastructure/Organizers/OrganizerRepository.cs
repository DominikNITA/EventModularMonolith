// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Security.Cryptography.X509Certificates;
using EventModularMonolith.Modules.Users.Domain.Organizers;
using EventModularMonolith.Modules.Users.Domain.Users;
using EventModularMonolith.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Users.Infrastructure.Organizers;

internal sealed class OrganizerRepository(UsersDbContext context) : IOrganizerRepository
{
   public async Task<Organizer> GetByIdAsync(OrganizerId id, CancellationToken cancellationToken = default)
   {
      return await context.Set<Organizer>().SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
   }

   public async Task<bool> IsUserModeratingAnyOrganizerAsync(UserId userId, CancellationToken cancellationToken = default)
   {
      return await context.Organizers.AnyAsync(o => o.Moderators.Any(m => m.UserId == userId && m.IsActive), cancellationToken);
   }

   public async Task InsertAsync(Organizer organizer, CancellationToken cancellationToken = default)
   {
      await context.Set<Organizer>().AddAsync(organizer, cancellationToken);
   }
}
