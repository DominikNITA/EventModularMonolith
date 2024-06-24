using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Application.Categories.CreateCategory;
using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Shared.Application.Clock;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Application.Categories.CreateCategory;

internal sealed class CreateCategoryCommandHandler(
   ICategoryRepository categoryRepository,
   IUnitOfWork unitOfWork) : ICommandHandler<CreateCategoryCommand, Guid>
{
   public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
   {
      Result<Category> result = Category.Create(
         request.Name);

      if (result.IsFailure)
      {
         return Result.Failure<Guid>(result.Error);
      }

      categoryRepository.Insert(result.Value);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      return result.Value.Id;
   }
}
