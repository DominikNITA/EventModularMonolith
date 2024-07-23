// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Ticketing.Application.Customers.CreateCustomer;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Ticketing.Presentation.Customers;

internal sealed class CreateCustomer : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPost("customers", async (CreateCustomerRequest request, ISender sender) =>
         {
            var command = new CreateCustomerCommand(
                    request.Email,
    request.FirstName,
    request.LastName
            );

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
         })
      .WithTags(Tags.Customers);
   }

   internal sealed class CreateCustomerRequest
   {
          public string Email {get;set;} = string.Empty; 
    public string FirstName {get;set;} = string.Empty; 
    public string LastName {get;set;} = string.Empty; 

   }
}
