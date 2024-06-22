using EventModularMonolith.Shared.Domain;
using MediatR;

namespace EventModularMonolith.Shared.Application.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;
