﻿using FluentValidation;

namespace EventModularMonolith.Modules.Events.Application.Categories.CreateCategory;

internal sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
   public CreateCategoryCommandValidator()
   {
      RuleFor(c => c.Name).NotEmpty();
   }
}
