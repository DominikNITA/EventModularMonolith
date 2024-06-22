using EventModularMonolith.Shared.Domain;
using MediatR;

namespace EventModularMonolith.Shared.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
