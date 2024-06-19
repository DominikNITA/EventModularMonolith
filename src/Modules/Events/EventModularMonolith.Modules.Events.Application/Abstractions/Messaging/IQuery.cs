using EventModularMonolith.Modules.Events.Domain.Abstractions;
using MediatR;

namespace EventModularMonolith.Modules.Events.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
