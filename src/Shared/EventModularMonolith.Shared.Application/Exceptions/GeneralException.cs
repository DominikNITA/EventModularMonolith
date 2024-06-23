using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Shared.Application.Exceptions;

public sealed class GeneralException : Exception
{
   public GeneralException(string requestName, Error? error = default, Exception? innerException = default) : base("Application exception", innerException)
   {
      RequestName = requestName;
      Error = error;
   }

   public string RequestName { get;  }
   public Error? Error { get; }
}
