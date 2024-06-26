using System.Reflection.Metadata;
using EventModularMonolith.Modules.Ticketing.Application.Carts;
using EventModularMonolith.Shared.Presentation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventModularMonolith.Modules.Ticketing.Infrastructure;

public static class TicketingModule
{
   public static IServiceCollection AddTicketingModule(
      this IServiceCollection services,
      IConfiguration configuration)
   {
      services.AddInfrastructure(configuration);

      services.AddEndpoints(Presentation.AssemblyReference.Assembly);

      return services;
   }

#pragma warning disable S1172 // Unused method parameters should be removed
#pragma warning disable IDE0060
   private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
#pragma warning restore IDE0060
#pragma warning restore S1172 // Unused method parameters should be removed
   {
      services.AddSingleton<CartService>();
   }
}
