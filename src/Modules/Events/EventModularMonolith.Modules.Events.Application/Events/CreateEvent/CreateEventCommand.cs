using EventModularMonolith.Shared.Application.Messaging;
using FluentValidation;

namespace EventModularMonolith.Modules.Events.Application.Events.CreateEvent;

public sealed record CreateEventCommand(
   Guid CategoryId,
   string Title,
   string Description,
   string Location,
   DateTime StartsAtUtc,
   DateTime? EndsAtUtc) : ICommand<Guid>;


internal sealed class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
   public CreateEventCommandValidator()
   {
      RuleFor(c => c.Title).NotEmpty();
      RuleFor(c => c.Description).NotEmpty();
      RuleFor(c => c.Location).NotEmpty();
      RuleFor(c => c.StartsAtUtc).NotEmpty();
      RuleFor(c => c.EndsAtUtc).Must((cmd, value) => value > cmd.StartsAtUtc).When(c => c.EndsAtUtc.HasValue);
   }
}
