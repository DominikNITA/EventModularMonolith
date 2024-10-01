using System.Security.Cryptography.X509Certificates;
using EventModularMonolith.Shared.Application.Storage;
using EventModularMonolith.Shared.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Shared.Presentation.Endpoints;

public class Files : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPost("/upload", async (IFormFile file, IBlobService blobService) =>
         {
            using Stream stream = file.OpenReadStream();

            string filePath = await blobService.UploadAsync(ContainerTags.Temporary, Guid.NewGuid(), stream, file.ContentType);

            return Results.Ok(filePath);
         })
         .WithTags("Files")
         .Accepts<MultipleFilesRequest>("multipart/form-data")
         .Produces<Result<string>>()
         .DisableAntiforgery()
         .WithName("UploadSingleFile");

      app.MapPost("/upload_many", async (IFormFileCollection myFiles, IBlobService blobService) =>
         {
            string[] filePaths = [];
            var tempContainerId = Guid.NewGuid();
            foreach (IFormFile file in myFiles)
            {
               await using Stream stream = file.OpenReadStream();
               filePaths = filePaths
                  .Append(await blobService.UploadAsync(ContainerTags.Temporary, tempContainerId, stream,
                     file.ContentType)).ToArray();
            }

            return Results.Ok(filePaths);
         })
      .WithTags("Files")
      .Accepts<MultipleFilesRequest>("multipart/form-data")
      .Produces<Result<string[]>>()
      .DisableAntiforgery()
         .WithName("UploadManyFiles");

      //app.MapGet("/files/sas_token", async () =>
      //{

      //})
   }
}

public record MultipleFilesRequest()
{
   public List<IFormFile> MyFiles { get; set; }
}
