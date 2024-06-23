using EventModularMonolith.Shared.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventModularMonolith.Shared.Application.Behaviors;

internal sealed class ExceptionHandlingPipelineBehavior<TRequest, TResponse>(
   ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> logger)
   : IPipelineBehavior<TRequest, TResponse>
   where TRequest : class
{
   public async Task<TResponse> Handle(
      TRequest request,
      RequestHandlerDelegate<TResponse> next,
      CancellationToken cancellationToken)
   {
      try
      {
         return await next();
      }
      catch (Exception exception)
      {
         logger.LogError(exception, "Unhandled exception for {RequestName}", typeof(TRequest).Name);

         throw new GeneralException(typeof(TRequest).Name, innerException: exception);
      }
   }
}
