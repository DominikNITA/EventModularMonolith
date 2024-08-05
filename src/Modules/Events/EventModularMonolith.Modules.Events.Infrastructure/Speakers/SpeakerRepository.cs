// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Domain.Speakers;
using EventModularMonolith.Modules.Events.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EventModularMonolith.Modules.Events.Infrastructure.Speakers;

internal sealed class SpeakerRepository(EventsDbContext context) : Repository<Speaker>(context), ISpeakerRepository
{
   public IEnumerable<Speaker> GetSpeakersForEvent(Guid eventId)
   {
      return context.Speakers.Where(s => s.Events.Any(e => e.Id == eventId));
   }

   public IEnumerable<Speaker> GetSpeakersByIds(IEnumerable<Guid> speakersIds)
   {
      return context.Speakers.Where(s => speakersIds.Contains(s.Id));
   }
}
