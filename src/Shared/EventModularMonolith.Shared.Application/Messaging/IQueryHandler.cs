using EventModularMonolith.Shared.Domain;
using MediatR;

namespace EventModularMonolith.Shared.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
