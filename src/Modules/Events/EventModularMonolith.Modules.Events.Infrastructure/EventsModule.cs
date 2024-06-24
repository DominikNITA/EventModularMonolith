using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Modules.Events.Domain.TicketTypes;
using EventModularMonolith.Modules.Events.Infrastructure.Categories;
using EventModularMonolith.Modules.Events.Infrastructure.Database;
using EventModularMonolith.Modules.Events.Infrastructure.Events;
using EventModularMonolith.Modules.Events.Infrastructure.TicketTypes;
using EventModularMonolith.Modules.Events.Presentation.Events;
using EventModularMonolith.Shared.Presentation;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventModularMonolith.Modules.Events.Infrastructure;

public static class EventsModule
{

   public static IServiceCollection AddEventsModule(this IServiceCollection services, IConfiguration configuration)
   {
      services.AddEndpoints(Presentation.AssemblyReference.Assembly);

      services.AddInfrastructure(configuration);

      return services;
   }

   private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
   {
      string databaseConnectionString = configuration.GetConnectionString("Database")!;

      services.AddDbContext<EventsDbContext>(options =>
         options.UseNpgsql(databaseConnectionString,
               npgsqlOptions => npgsqlOptions
                  .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
            .UseSnakeCaseNamingConvention()
            .AddInterceptors());

      services.AddScoped<IEventRepository, EventRepository>();
      services.AddScoped<ICategoryRepository, CategoryRepository>();
      services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();

      services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());
   }
}
