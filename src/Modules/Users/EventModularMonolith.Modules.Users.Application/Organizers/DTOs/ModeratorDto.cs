// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EventModularMonolith.Modules.Users.Application.Organizers.DTOs;

public class ModeratorDto
{
   public Guid UserId { get; set; }
   public string FirstName { get; set; } = string.Empty;
   public string LastName { get; set; } = string.Empty;
   public string Email { get; set; } = string.Empty;
   public bool IsOwner { get; set; }
   public bool IsActive { get; set; }
}

