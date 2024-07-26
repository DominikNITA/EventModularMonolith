// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EventModularMonolith.Modules.Ticketing.Application.Events.DTOs;

public class EventDto
{
    public Guid Id { get; set; }
    public System.Guid CustomerId {get;set;} 
    public string Title {get;set;} = string.Empty; 
    public string Description {get;set;} = string.Empty; 
    public string Location {get;set;} = string.Empty; 
    public DateTime StartsAtUtc {get;set;} 
    public DateTime? EndsAtUtc {get;set;} 
    public bool Canceled {get;set;} 

}

