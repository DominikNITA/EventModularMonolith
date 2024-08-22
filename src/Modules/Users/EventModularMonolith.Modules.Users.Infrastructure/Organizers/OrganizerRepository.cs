// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Users.Domain.Organizers;
using EventModularMonolith.Modules.Users.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Users.Infrastructure.Organizers;

internal sealed class OrganizerRepository(UsersDbContext context) : IOrganizerRepository
{
   public async Task<Organizer> GetByIdAsync(OrganizerId id, CancellationToken cancellationToken = default)
   {
      return await context.Set<Organizer>().SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
   }

   public async Task InsertAsync(Organizer organizer, CancellationToken cancellationToken = default)
   {
      await context.Set<Organizer>().AddAsync(organizer, cancellationToken);
   }
}
