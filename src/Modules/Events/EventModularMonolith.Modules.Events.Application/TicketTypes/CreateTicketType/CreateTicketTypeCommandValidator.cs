// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;

namespace EventModularMonolith.Modules.Events.Application.TicketTypes.CreateTicketType;

public class CreateTicketTypeCommandValidator : AbstractValidator<CreateTicketTypeCommand>
{
   public CreateTicketTypeCommandValidator()
   {
      RuleFor(v => v.Name).MaximumLength(256).NotEmpty();
      RuleFor(v => v.EventId).NotEmpty();
      RuleFor(v => v.Price).GreaterThanOrEqualTo(0);
      RuleFor(c => c.Currency).NotEmpty();
      RuleFor(c => c.Quantity).GreaterThan(decimal.Zero);
   }
}

