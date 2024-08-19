// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;


namespace EventModularMonolith.Modules.Events.Application.Categories.CreateCategory;
    
public class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateCategoryCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
           Result<Category> result = Category.Create(
             request.Name
           );

           if(result.IsFailure)
           {
                return Result.Failure<Guid>(result.Error);
           }

           await categoryRepository.InsertAsync(result.Value, cancellationToken);

           await unitOfWork.SaveChangesAsync(cancellationToken);

           return result.Value.Id.Value;
        }
    }

