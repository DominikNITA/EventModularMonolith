// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EventModularMonolith.Modules.Ticketing.Application.Customers.DTOs;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string Email {get;set;} = string.Empty; 
    public string FirstName {get;set;} = string.Empty; 
    public string LastName {get;set;} = string.Empty; 

}

