// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Application.Categories.UpdateCategory;

public class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<UpdateCategoryCommand>
{
   public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
   {
      Category? category = await categoryRepository.GetAsync(request.CategoryId, cancellationToken);

      if (category is null)
      {
         return Result.Failure(CategoryErrors.NotFound(request.CategoryId));
      }

      category.Update(request.Name);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return Result.Success();
   }
}

