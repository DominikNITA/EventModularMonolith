// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Ticketing.Application.Abstractions.Data;
using EventModularMonolith.Modules.Ticketing.Domain.Customers;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Ticketing.Application.Customers.UpdateCustomer;

public class UpdateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<UpdateCustomerCommand, Guid>
{
   public async Task<Result<Guid>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
   {
      Customer? customer = await customerRepository.GetAsync(request.Id, cancellationToken);

      if (customer is null)
      {
         return Result.Failure<Guid>(CustomerErrors.NotFound(request.Id));
      }

      customer.Update(request.FirstName, request.LastName);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return customer.Id;
   }
}

