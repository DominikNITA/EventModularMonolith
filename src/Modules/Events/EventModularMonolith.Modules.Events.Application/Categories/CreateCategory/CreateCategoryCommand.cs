using EventModularMonolith.Shared.Application.Messaging;

namespace EventModularMonolith.Modules.Events.Application.Categories.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : ICommand<Guid>;
