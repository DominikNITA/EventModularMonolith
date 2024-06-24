using EventModularMonolith.Modules.Events.Application.Categories.CreateCategory;
using EventModularMonolith.Modules.Events.Application.Events.CreateEvent;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Events.Presentation.Categories;

internal sealed class CreateCategory : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPost("categories", async (CreateCategoryRequest request, ISender sender) =>
         {
            var command = new CreateCategoryCommand(request.Name);

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
         })
      .WithTags(Tags.Categories);
   }

   internal sealed class CreateCategoryRequest
   {
      public string Name { get; set; }

   }
}
