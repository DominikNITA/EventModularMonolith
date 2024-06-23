using EventModularMonolith.Api.Extensions;
using EventModularMonolith.Api.Middleware;
using EventModularMonolith.Modules.Events.Infrastructure;
using EventModularMonolith.Shared.Application;
using EventModularMonolith.Shared.Infrastructure;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication([EventModularMonolith.Modules.Events.Application.AssemblyReference.Assembly]);

builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("Database")!, builder.Configuration.GetConnectionString("Cache")!);

// TODO: Create base module class which contains abstract module name and assembly
builder.Configuration.AddModuleConfigurations(["events"]);

builder.Services.AddEventsModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();

   app.ApplyMigrations();
}


EventsModule.MapEndpoints(app);

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.Run();
