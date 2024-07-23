// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Ticketing.Application.Customers.GetAllCustomers;
using EventModularMonolith.Modules.Ticketing.Application.Customers.DTOs;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Ticketing.Presentation.Customers;

internal sealed class GetAllCustomers : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPost("customers", async (ISender sender) =>
         {
            Result<IReadOnlyCollection<CustomerDto>> result = await sender.Send(new GetAllCustomersQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
         })
      .WithTags(Tags.Customers);
   }
}