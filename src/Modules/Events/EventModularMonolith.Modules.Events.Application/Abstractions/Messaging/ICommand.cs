using EventModularMonolith.Modules.Events.Domain.Abstractions;
using MediatR;

namespace EventModularMonolith.Modules.Events.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;
