// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Ticketing.Application.Customers.DTOs;
using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Ticketing.Application.Customers.GetAllCustomers;

public sealed record GetAllCustomersQuery : IQuery<IReadOnlyCollection<CustomerDto>>;