// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Users.Application.Users.RegisterUser;
using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Users.Application.Organizers.CreateOrganizer;

public sealed record CreateOrganizerCommand(
    string Name, 
    string Description,
    RegisterUserCommand UserRegistration
) : ICommand<Guid>; 

