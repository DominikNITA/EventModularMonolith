﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.Speakers.DTOs;
using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Events.Application.Speakers.CreateSpeaker;

public sealed record CreateSpeakerCommand(
    string Name, 
    string Description,
    List<SpeakerLinkDto> Links): ICommand<Guid>; 

