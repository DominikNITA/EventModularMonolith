// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EventModularMonolith.Modules.Events.Application.TicketTypes.DTOs;

public class TicketTypeDto
{
   public Guid Id { get; set; }
   public Guid EventId { get; set; }
   public string Name { get; set; } = string.Empty;
   public decimal Price { get; set; }
   public string? Currency { get; set; }
   public decimal Quantity { get; set; }

}

