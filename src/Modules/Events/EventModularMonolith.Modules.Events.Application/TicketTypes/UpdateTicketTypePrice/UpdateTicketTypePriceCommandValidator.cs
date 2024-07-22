// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;

namespace EventModularMonolith.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;

public class UpdateTicketTypePriceCommandValidator : AbstractValidator<UpdateTicketTypePriceCommand>
{
   public UpdateTicketTypePriceCommandValidator()
   {
      RuleFor(v => v.TicketTypeId).NotEmpty();
      RuleFor(v => v.Price).GreaterThanOrEqualTo(0);
   }
}

