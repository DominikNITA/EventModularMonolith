using System.Data.Common;
using EventModularMonolith.Shared.Application.Data;
using Npgsql;

namespace EventModularMonolith.Shared.Infrastructure.Database;

public sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
   public async ValueTask<DbConnection> OpenConnectionAsync()
   {
      return await dataSource.OpenConnectionAsync();
   }
}
