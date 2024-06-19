using System.Data.Common;

namespace EventModularMonolith.Modules.Events.Application.Abstractions.Data;

public interface IDbConnectionFactory
{
   ValueTask<DbConnection> OpenConnectionAsync();
}
