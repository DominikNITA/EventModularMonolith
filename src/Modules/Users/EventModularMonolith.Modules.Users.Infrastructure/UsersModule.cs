using EventModularMonolith.Modules.Users.Application.Abstractions.Data;
using EventModularMonolith.Modules.Users.Domain.Users;
using EventModularMonolith.Modules.Users.Infrastructure.Database;
using EventModularMonolith.Modules.Users.Infrastructure.PublicApi;
using EventModularMonolith.Modules.Users.Infrastructure.Users;
using EventModularMonolith.Modules.Users.PublicApi;
using EventModularMonolith.Shared.Infrastructure.Interceptors;
using EventModularMonolith.Shared.Presentation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventModularMonolith.Modules.Users.Infrastructure;

public static class UsersModule
{
   public static IServiceCollection AddUsersModule(
      this IServiceCollection services,
      IConfiguration configuration)
   {
      services.AddInfrastructure(configuration);

      services.AddEndpoints(Presentation.AssemblyReference.Assembly);

      return services;
   }

   private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
   {
      services.AddDbContext<UsersDbContext>((sp, options) =>
         options
            .UseNpgsql(
               configuration.GetConnectionString("Database"),
               npgsqlOptions => npgsqlOptions
                  .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
            .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>())
            .UseSnakeCaseNamingConvention());

      services.AddScoped<IUserRepository, UserRepository>();

      services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());

      services.AddScoped<IUsersApi, UsersApi>();
   }
}
