using EventModularMonolith.Shared.Application.Caching;
using EventModularMonolith.Shared.Application.Clock;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Infrastructure.Caching;
using EventModularMonolith.Shared.Infrastructure.Clock;
using EventModularMonolith.Shared.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Interceptors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using StackExchange.Redis;

namespace EventModularMonolith.Shared.Infrastructure;

public static class InfrastructureConfiguration
{
   public static IServiceCollection AddInfrastructure(this IServiceCollection services, string databaseConnectionString, string redisConnectionString)
   {
      NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
      services.TryAddSingleton(npgsqlDataSource);

      services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

      services.TryAddSingleton<PublishDomainEventsInterceptor>();

      services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

      IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
      services.TryAddSingleton(connectionMultiplexer);

      services.AddStackExchangeRedisCache(options =>
         options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));

      services.TryAddSingleton<ICacheService, CacheService>();

      return services;
   }
}
