using FluentValidation;

namespace EventModularMonolith.Modules.Events.Application.Events.CreateEvent;

internal sealed class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
   public CreateEventCommandValidator()
   {
      RuleFor(c => c.Title).NotEmpty();
      RuleFor(c => c.Description).NotEmpty();
      RuleFor(c => c.VenueId).NotEmpty();
      RuleFor(c => c.StartsAtUtc).NotEmpty();
      RuleFor(c => c.EndsAtUtc).Must((cmd, value) => value > cmd.StartsAtUtc).When(c => c.EndsAtUtc.HasValue);
   }
}
