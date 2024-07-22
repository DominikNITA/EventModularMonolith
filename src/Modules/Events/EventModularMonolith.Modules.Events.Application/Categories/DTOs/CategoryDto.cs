// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EventModularMonolith.Modules.Events.Application.Categories.DTOs;

public class CategoryDto
{
   public int Id { get; set; }
   public string Name { get; set; } = string.Empty;
   public bool IsArchived { get; set; }
}

