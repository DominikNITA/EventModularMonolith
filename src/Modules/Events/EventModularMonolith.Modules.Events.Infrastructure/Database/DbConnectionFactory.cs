using System.Data.Common;
using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using Npgsql;

namespace EventModularMonolith.Modules.Events.Infrastructure.Database;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
   public async ValueTask<DbConnection> OpenConnectionAsync()
   {
      return await dataSource.OpenConnectionAsync();
   }
}
