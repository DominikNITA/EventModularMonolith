using EventModularMonolith.Api.Extensions;
using EventModularMonolith.Modules.Events.Infrastructure;
using EventModularMonolith.Shared.Application;
using EventModularMonolith.Shared.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddApplication([EventModularMonolith.Modules.Events.Application.AssemblyReference.Assembly]);

builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("Database")!);

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

app.Run();
