// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.Speakers;

public interface ISpeakerRepository
{
   Task InsertAsync(Speaker speaker, CancellationToken cancellationToken = default);

   Task<Speaker> GetByIdAsync(SpeakerId id, CancellationToken cancellationToken = default);

   IEnumerable<Speaker> GetSpeakersForEvent(EventId eventId);

   IEnumerable<Speaker> GetSpeakersByIds(IEnumerable<SpeakerId> speakersIds);
}
