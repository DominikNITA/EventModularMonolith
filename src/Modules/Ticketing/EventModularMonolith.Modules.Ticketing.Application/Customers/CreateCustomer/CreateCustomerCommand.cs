// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Ticketing.Application.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(
    Guid Id,
    string Email,
    string FirstName,
    string LastName
): ICommand<Guid>; 

