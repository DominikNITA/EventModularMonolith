using EventModularMonolith.Shared.Application.Caching;
using EventModularMonolith.Shared.Application.Clock;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.EventBus;
using EventModularMonolith.Shared.Application.Storage;
using EventModularMonolith.Shared.Infrastructure.Caching;
using EventModularMonolith.Shared.Infrastructure.Clock;
using EventModularMonolith.Shared.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Outbox;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Quartz;
using StackExchange.Redis;
using Azure.Storage.Blobs;
using EventModularMonolith.Shared.Infrastructure.Storage;
using EventModularMonolith.Shared.Presentation;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Azure.Storage;

namespace EventModularMonolith.Shared.Infrastructure;

public static class InfrastructureConfiguration
{
   public static IServiceCollection AddInfrastructure(
      this IServiceCollection services,
      Action<IRegistrationConfigurator>[] moduleConfigureConsumers,
      string databaseConnectionString,
      string redisConnectionString,
      ConfigurationManager configuration)
   {
      services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

      services.TryAddSingleton<IEventBus, EventBus.EventBus>();

      services.TryAddSingleton<InsertOutboxMessagesInterceptor>();

      NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
      services.TryAddSingleton(npgsqlDataSource);

      services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

      services.AddQuartz();

      services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

      try
      {
         IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
         services.TryAddSingleton(connectionMultiplexer);

         services.AddStackExchangeRedisCache(options =>
            options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));
      }
      catch
      {
         services.AddDistributedMemoryCache();
      }

      services.TryAddSingleton<ICacheService, CacheService>();

      services.AddMassTransit(configure =>
      {
         foreach (Action<IRegistrationConfigurator> configureConsumers in moduleConfigureConsumers)
         {
            configureConsumers(configure);
         }

         configure.SetKebabCaseEndpointNameFormatter();

         configure.UsingInMemory((context, cfg) =>
         {
            cfg.ConfigureEndpoints(context);
         });
      });

      AzureBlobServiceConfig azureBlobServiceConfig = configuration.GetSection("AzureBlobServiceConfig").Get<AzureBlobServiceConfig>() ?? throw new Exception("AzureBlobServiceConfig is null");
      services.AddAzureClients(builder => { builder.AddBlobServiceClient(azureBlobServiceConfig.BaseUrl, new StorageSharedKeyCredential(azureBlobServiceConfig.AccountName, azureBlobServiceConfig.Password)); });
      services.AddSingleton<IBlobService, BlobService>();

      services.AddEndpoints(Presentation.AssemblyReference.Assembly);

      return services;
   }

   public record AzureBlobServiceConfig(Uri BaseUrl, string AccountName, string Password);
}
