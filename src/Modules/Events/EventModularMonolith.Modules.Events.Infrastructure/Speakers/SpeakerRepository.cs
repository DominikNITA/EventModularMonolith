// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Linq;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Modules.Events.Domain.Speakers;
using EventModularMonolith.Modules.Events.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Events.Infrastructure.Speakers;


internal sealed class SpeakerRepository(EventsDbContext context) : ISpeakerRepository
{
   public async Task<Speaker> GetByIdAsync(SpeakerId id, CancellationToken cancellationToken = default)
   {
      return await context.Speakers.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
   }

   public async Task InsertAsync(Speaker speaker, CancellationToken cancellationToken = default)
   {
      await context.Set<Speaker>().AddAsync(speaker, cancellationToken);
   }

   public IEnumerable<Speaker> GetSpeakersForEvent(EventId eventId)
   {
      return context.Speakers.Where(s => s.Events.Any(e => e.Id == eventId));
   }

   public IEnumerable<Speaker> GetSpeakersByIds(IEnumerable<SpeakerId> speakersIds)
   {
      return context.Speakers.Where(s => speakersIds.Contains(s.Id));
   }
}
