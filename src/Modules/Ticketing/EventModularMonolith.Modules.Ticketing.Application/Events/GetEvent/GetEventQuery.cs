// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Ticketing.Application.Events.DTOs;
using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Ticketing.Application.Events.GetEvent;

public sealed record GetEventQuery(Guid EventId) : IQuery<EventDto>;
