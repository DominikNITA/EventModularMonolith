using EventModularMonolith.Api.Extensions;
using EventModularMonolith.Api.Middleware;
using EventModularMonolith.Modules.Events.Infrastructure;
using EventModularMonolith.Modules.Users.Infrastructure;
using EventModularMonolith.Shared.Application;
using EventModularMonolith.Shared.Infrastructure;
using EventModularMonolith.Shared.Presentation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication([
   EventModularMonolith.Modules.Events.Application.AssemblyReference.Assembly,
   EventModularMonolith.Modules.Users.Application.AssemblyReference.Assembly
]);

string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;
builder.Services.AddInfrastructure(databaseConnectionString, redisConnectionString);

// TODO: Create base module class which contains abstract module name and assembly
builder.Configuration.AddModuleConfigurations(["events","users"]);

builder.Services.AddHealthChecks()
   .AddNpgSql(databaseConnectionString)
   .AddRedis(redisConnectionString);

builder.Services.AddEventsModule(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();

   app.ApplyMigrations();
}


app.MapEndpoints();

app.MapHealthChecks("health", new HealthCheckOptions()
{
   ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.Run();
