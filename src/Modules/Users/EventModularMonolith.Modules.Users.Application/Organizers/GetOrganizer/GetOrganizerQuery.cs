﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Users.Application.Organizers.DTOs;
using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Users.Application.Organizers.GetOrganizer;

public sealed record GetOrganizerQuery(Guid OrganizerId) : IQuery<OrganizerDto>;
