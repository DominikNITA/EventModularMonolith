// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EventModularMonolith.Modules.Users.Application.Organizers.DTOs;

public class OrganizerDto
{
    public Guid Id { get; set; }
    public string Name {get;set;} = string.Empty; 
    public string Description {get;set;} = string.Empty; 

}

