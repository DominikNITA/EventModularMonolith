// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.Speakers;

public interface ISpeakerRepository : IRepository<Speaker>
{
   IEnumerable<Speaker> GetSpeakersForEvent(Guid eventId);

   IEnumerable<Speaker> GetSpeakersByIds(IEnumerable<Guid> speakersIds);
}
