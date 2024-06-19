using EventModularMonolith.Modules.Events.Application.Abstractions.Messaging;
using FluentValidation;
using MediatR;

namespace EventModularMonolith.Modules.Events.Application.Events.CreateEvent;

public sealed record CreateEventCommand(
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
