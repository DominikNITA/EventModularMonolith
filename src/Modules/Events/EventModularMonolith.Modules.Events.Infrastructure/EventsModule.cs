using EventModularMonolith.Modules.Events.Application;
using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Application.Events;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Modules.Events.Infrastructure.Database;
using EventModularMonolith.Modules.Events.Infrastructure.Events;
using EventModularMonolith.Modules.Events.Presentation.Events;
using FluentValidation;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace EventModularMonolith.Modules.Events.Infrastructure;

public static class EventsModule
{
   public static void MapEndpoints(IEndpointRouteBuilder app)
   {
      EventEndpoints.MapEndpoints(app);
   }

   public static IServiceCollection AddEventsModule(this IServiceCollection services, IConfiguration configuration)
   {
      services.AddMediatR(config =>
      {
         config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);
      });

      services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);

      services.AddInfrastructure(configuration);

      return services;
   }

   private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
   {
      string databaseConnectionString = configuration.GetConnectionString("Database")!;

      NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
      services.TryAddSingleton(npgsqlDataSource);

      services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

      services.AddDbContext<EventsDbContext>(options =>
         options.UseNpgsql(databaseConnectionString,
               npgsqlOptions => npgsqlOptions
                  .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
            .UseSnakeCaseNamingConvention());

      services.AddScoped<IEventRepository, EventRepository>();

      services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());
   }
}
