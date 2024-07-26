﻿using EventModularMonolith.Modules.Events.IntegrationEvents;
using EventModularMonolith.Modules.Ticketing.Application.Abstractions.Data;
using EventModularMonolith.Modules.Ticketing.Application.Carts;
using EventModularMonolith.Modules.Ticketing.Domain.Customers;
using EventModularMonolith.Modules.Ticketing.Domain.Events;
using EventModularMonolith.Modules.Ticketing.Domain.TicketTypes;
using EventModularMonolith.Modules.Ticketing.Infrastructure.Customers;
using EventModularMonolith.Modules.Ticketing.Infrastructure.Database;
using EventModularMonolith.Modules.Ticketing.Infrastructure.Events;
using EventModularMonolith.Modules.Ticketing.Infrastructure.Inbox;
using EventModularMonolith.Modules.Ticketing.Infrastructure.Outbox;
using EventModularMonolith.Modules.Ticketing.Infrastructure.TicketTypes;
using EventModularMonolith.Modules.Users.IntegrationEvents;
using EventModularMonolith.Shared.Application.EventBus;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Infrastructure.Outbox;
using EventModularMonolith.Shared.Presentation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EventModularMonolith.Modules.Ticketing.Infrastructure;

public static class TicketingModule
{
   public static IServiceCollection AddTicketingModule(
      this IServiceCollection services,
      IConfiguration configuration)
   {
      services.AddDomainEventHandlers();

      services.AddIntegrationEventHandlers();

      services.AddInfrastructure(configuration);

      services.AddEndpoints(Presentation.AssemblyReference.Assembly);

      return services;
   }

   public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
   {
      registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserRegisteredIntegrationEvent>>();
      registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserProfileUpdatedIntegrationEvent>>();
      registrationConfigurator.AddConsumer<IntegrationEventConsumer<EventPublishedIntegrationEvent>>();
      registrationConfigurator.AddConsumer<IntegrationEventConsumer<TicketTypePriceChangedIntegrationEvent>>();
   }

   private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
   {
      services.AddDbContext<TicketingDbContext>((sp, options) =>
         options
            .UseNpgsql(
               configuration.GetConnectionString("Database"),
               npgsqlOptions => npgsqlOptions
                  .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Ticketing))
            .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
            .UseSnakeCaseNamingConvention());

      services.AddScoped<ICustomerRepository, CustomerRepository>();
      services.AddScoped<IEventRepository, EventRepository>();
      services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();

      services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TicketingDbContext>());

      services.AddSingleton<CartService>();

      services.Configure<OutboxOptions>(configuration.GetSection("Ticketing:Outbox"));

      services.ConfigureOptions<ConfigureProcessOutboxJob>();

      services.Configure<InboxOptions>(configuration.GetSection("Ticketing:Inbox"));

      services.ConfigureOptions<ConfigureProcessInboxJob>();
   }

   private static void AddDomainEventHandlers(this IServiceCollection services)
   {
      Type[] domainEventHandlers = Application.AssemblyReference.Assembly
         .GetTypes()
         .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler)))
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
