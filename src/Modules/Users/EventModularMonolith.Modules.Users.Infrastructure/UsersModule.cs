using EventModularMonolith.Modules.Users.Application.Abstractions.Data;
using EventModularMonolith.Modules.Users.Application.Abstractions.Identity;
using EventModularMonolith.Modules.Users.Domain.Organizers;
using EventModularMonolith.Modules.Users.Domain.Users;
using EventModularMonolith.Modules.Users.Infrastructure.Database;
using EventModularMonolith.Modules.Users.Infrastructure.Identity;
using EventModularMonolith.Modules.Users.Infrastructure.Inbox;
using EventModularMonolith.Modules.Users.Infrastructure.Organizers;
using EventModularMonolith.Modules.Users.Infrastructure.Outbox;
using EventModularMonolith.Modules.Users.Infrastructure.PublicApi;
using EventModularMonolith.Modules.Users.Infrastructure.Users;
using EventModularMonolith.Modules.Users.PublicApi;
using EventModularMonolith.Shared.Application.EventBus;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Outbox;
using EventModularMonolith.Shared.Presentation;
using MassTransit.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace EventModularMonolith.Modules.Users.Infrastructure;

public static class UsersModule
{
   public static IServiceCollection AddUsersModule(
      this IServiceCollection services,
      IConfiguration configuration)
   {
      services.AddDomainEventHandlers();

      services.AddIntegrationEventHandlers();

      services.AddInfrastructure(configuration);

      services.AddEndpoints(Presentation.AssemblyReference.Assembly);

      return services;
   }

   private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
   {
      services.Configure<KeyCloakOptions>(configuration.GetSection("Users:KeyCloak"));

      services.AddTransient<KeyCloakAuthDelegatingHandler>();

      services.AddHttpClient<KeyCloakClient>((serviceProvider, httpClient) =>
         {
            KeyCloakOptions keyCloakOptions = serviceProvider.GetRequiredService<IOptions<KeyCloakOptions>>().Value;

            httpClient.BaseAddress = new Uri(keyCloakOptions.AdminUrl);
         })
         .AddHttpMessageHandler<KeyCloakAuthDelegatingHandler>();

      services.AddTransient<IIdentityProviderService, IdentityProviderService>();

      services.AddDbContext<UsersDbContext>((sp, options) =>
         {
            options
               .UseNpgsql(
                  configuration.GetConnectionString("Database"),
                  npgsqlOptions => npgsqlOptions
                     .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
               .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
               .UseSnakeCaseNamingConvention();
            options.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

         }
      );

      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IOrganizerRepository, OrganizerRepository>();

      services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());

      services.AddScoped<IUsersApi, UsersApi>();

      services.Configure<OutboxOptions>(configuration.GetSection("Users:Outbox"));

      services.ConfigureOptions<ConfigureProcessOutboxJob>();

      services.Configure<InboxOptions>(configuration.GetSection("Users:Inbox"));

      services.ConfigureOptions<ConfigureProcessInboxJob>();
   }

   private static void AddDomainEventHandlers(this IServiceCollection services)
   {
      Type[] domainEventHandlers = Application.AssemblyReference.Assembly
         .GetTypes()
         .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler)))
         .ToArray();

      Type[] domainEventHandlers2 = Application.AssemblyReference.Assembly
         .GetTypes().ToArray();

      Console.WriteLine(domainEventHandlers2.Length);

      foreach (Type domainEventHandler in domainEventHandlers)
      {
         services.TryAddScoped(domainEventHandler);

         Type domainEvent = domainEventHandler
            .GetInterfaces()
            .Single(i => i.IsGenericType)
            .GetGenericArguments()
            .Single();

         Type closedIdempotentHandler = typeof(IdempotentDomainEventHandler<>).MakeGenericType(domainEvent);

         services.Decorate(domainEventHandler, closedIdempotentHandler);
      }
   }

   private static void AddIntegrationEventHandlers(this IServiceCollection services)
   {
      Type[] integrationEventHandlers = Presentation.AssemblyReference.Assembly
         .GetTypes()
         .Where(t => t.IsAssignableTo(typeof(IIntegrationEventHandler)))
         .ToArray();

      foreach (Type integrationEventHandler in integrationEventHandlers)
      {
         services.TryAddScoped(integrationEventHandler);

         Type integrationEvent = integrationEventHandler
            .GetInterfaces()
            .Single(i => i.IsGenericType)
            .GetGenericArguments()
            .Single();

         Type closedIdempotentHandler =
            typeof(IdempotentIntegrationEventHandler<>).MakeGenericType(integrationEvent);

         services.Decorate(integrationEventHandler, closedIdempotentHandler);
      }
   }
}
