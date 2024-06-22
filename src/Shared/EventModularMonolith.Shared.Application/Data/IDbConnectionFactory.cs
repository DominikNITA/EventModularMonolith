using System.Data.Common;

namespace EventModularMonolith.Shared.Application.Data;

public interface IDbConnectionFactory
{
   ValueTask<DbConnection> OpenConnectionAsync();
}
