using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Domain.Categories;
using EventModularMonolith.Modules.Events.Domain.Events;
using EventModularMonolith.Modules.Events.Domain.Organizers;
using EventModularMonolith.Modules.Events.Domain.Speakers;
using EventModularMonolith.Modules.Events.Domain.TicketTypes;
using EventModularMonolith.Modules.Events.Domain.Venues;
using EventModularMonolith.Modules.Events.Infrastructure.Categories;
using EventModularMonolith.Modules.Events.Infrastructure.Database;
using EventModularMonolith.Modules.Events.Infrastructure.Events;
using EventModularMonolith.Modules.Events.Infrastructure.Inbox;
using EventModularMonolith.Modules.Events.Infrastructure.Organizers;
using EventModularMonolith.Modules.Events.Infrastructure.Outbox;
using EventModularMonolith.Modules.Events.Infrastructure.Speakers;
using EventModularMonolith.Modules.Events.Infrastructure.TicketTypes;
using EventModularMonolith.Modules.Events.Infrastructure.Venues;
using EventModularMonolith.Modules.Users.IntegrationEvents;
using EventModularMonolith.Shared.Application.EventBus;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Infrastructure.Database;
using EventModularMonolith.Shared.Infrastructure.Outbox;
using EventModularMonolith.Shared.Presentation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EventModularMonolith.Modules.Events.Infrastructure;

public static class EventsModule
{

   public static IServiceCollection AddEventsModule(this IServiceCollection services, IConfiguration configuration)
   {
      services.AddDomainEventHandlers();

      services.AddIntegrationEventHandlers();

      services.AddEndpoints(Presentation.AssemblyReference.Assembly);

      services.AddInfrastructure(configuration);

      return services;
   }

   public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
   {
      registrationConfigurator.AddConsumer<IntegrationEventConsumer<OrganizerCreatedIntegrationEvent>>();
   }

   private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
   {
      string databaseConnectionString = configuration.GetConnectionString("Database")!;

      services.AddDbContext<EventsDbContext>((sp,options) =>
         {
            options.UseNpgsql(databaseConnectionString,
                  npgsqlOptions => npgsqlOptions
                     .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
               .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
               .UseSnakeCaseNamingConvention();
            options.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();
         }
      );

      services.AddScoped<IEventRepository, EventRepository>();
      services.AddScoped<ICategoryRepository, CategoryRepository>();
      services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
      services.AddScoped<IVenueRepository, VenueRepository>();
      services.AddScoped<ISpeakerRepository, SpeakerRepository>();
      services.AddScoped<IOrganizerRepository, OrganizerRepository>();

      services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());

      services.Configure<OutboxOptions>(configuration.GetSection("Events:Outbox"));

      services.ConfigureOptions<ConfigureProcessOutboxJob>();

      services.Configure<InboxOptions>(configuration.GetSection("Events:Inbox"));

      services.ConfigureOptions<ConfigureProcessInboxJob>();
   }

   private static void AddDomainEventHandlers(this IServiceCollection services)
   {
      Type[] domainEventHandlers = Application.AssemblyReference.Assembly
         .GetTypes()
         .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler<>)))
         .ToArray();

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
