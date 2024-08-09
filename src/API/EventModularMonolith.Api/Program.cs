using EventModularMonolith.Api.Extensions;
using EventModularMonolith.Api.Middleware;
using EventModularMonolith.Modules.Events.Infrastructure;
using EventModularMonolith.Modules.Ticketing.Infrastructure;
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
builder.Services.AddSwaggerGen(options => { options.CustomSchemaIds(type => type.ToString().Replace("+", ".")); });
builder.Services.AddOpenApiDocument();
builder.Services.AddApplication([
   EventModularMonolith.Modules.Events.Application.AssemblyReference.Assembly,
   EventModularMonolith.Modules.Users.Application.AssemblyReference.Assembly,
   EventModularMonolith.Modules.Ticketing.Application.AssemblyReference.Assembly,
]);

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
   builder.WithOrigins("*")
      .AllowAnyMethod()
      .AllowAnyHeader();
}));

string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;
builder.Services.AddInfrastructure(
   [TicketingModule.ConfigureConsumers],
   databaseConnectionString, redisConnectionString, builder.Configuration);

// TODO: Create base module class which contains abstract module name and assembly
builder.Configuration.AddModuleConfigurations(["events", "users", "ticketing"]);

builder.Services.AddHealthChecks()
   .AddNpgSql(databaseConnectionString)
   .AddRedis(redisConnectionString);

builder.Services.AddEventsModule(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddTicketingModule(builder.Configuration);

WebApplication app = builder.Build();

app.UseCors("MyPolicy");

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

RouteGroupBuilder apiGroup = app.MapGroup("/api");

app.MapEndpoints(apiGroup);

app.MapHealthChecks("health", new HealthCheckOptions()
{
   ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.Run();
