using EventModularMonolith.Shared.Application.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Shared.Presentation.Endpoints;

public class Files : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPost("files", async (IFormFile file, IBlobService blobService) =>
         {
            using Stream stream = file.OpenReadStream();

            string filePath = await blobService.UploadAsync(ContainerTags.Temporary, Guid.NewGuid(), stream, file.ContentType);

            return Results.Ok(filePath);
         })
         .WithTags("Files")
         .DisableAntiforgery();
   }
}
