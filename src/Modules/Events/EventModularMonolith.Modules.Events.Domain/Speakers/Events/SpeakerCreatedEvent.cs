// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Domain.Speakers.Events;

public sealed class SpeakerCreatedDomainEvent(Guid speakerId) : DomainEvent
{
    public Guid SpeakerId { get; init;} = speakerId;
}

