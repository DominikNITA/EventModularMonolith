// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Ticketing.Application.Abstractions.Data;
using EventModularMonolith.Modules.Ticketing.Domain.Customers;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;


namespace EventModularMonolith.Modules.Ticketing.Application.Customers.CreateCustomer;

public class CreateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateCustomerCommand, Guid>
{
   public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
   {
      Result<Customer> customer = Customer.Create(
           request.Id,
           request.Email,
           request.FirstName,
           request.LastName
      );

      if (customer.IsFailure)
      {
         return Result.Failure<Guid>(customer.Error);
      }

      await customerRepository.InsertAsync(customer.Value, cancellationToken);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return customer.Value.Id.Value;
   }
}

