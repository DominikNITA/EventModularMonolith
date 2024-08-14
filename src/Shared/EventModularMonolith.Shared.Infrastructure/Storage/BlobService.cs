using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EventModularMonolith.Shared.Application.Storage;

namespace EventModularMonolith.Shared.Infrastructure.Storage;

internal sealed class BlobService(BlobServiceClient blobServiceClient) : IBlobService
{
   private static string GetContainerName(string containerType, Guid id)
   {
      return $"{containerType}-{id}";
   }

   public async Task<string> UploadAsync(string containerType, Guid id, Stream stream, string contentType, CancellationToken cancellationToken = default)
   {
      BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(GetContainerName(containerType, id));

      await containerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer, cancellationToken: cancellationToken);

      var fileId = Guid.NewGuid();
      BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

      await blobClient.UploadAsync(
         stream,
         new BlobHttpHeaders() { ContentType = contentType },
         cancellationToken: cancellationToken
      );

      return containerClient.Name;
   }

   public async Task<IReadOnlyCollection<string>> GetUrlsFromContainerAsync(string containerType, Guid id, CancellationToken cancellationToken = default)
   {
      BlobContainerClient? containerClient = blobServiceClient.GetBlobContainerClient(GetContainerName(containerType, id));

      var urls = new List<string>();

      Response<bool>? containerExists = await containerClient.ExistsAsync(cancellationToken);
      if (!containerExists.Value)
      {
         return urls;
      }

      await foreach (BlobItem blobItem in containerClient.GetBlobsAsync(cancellationToken: cancellationToken))
      {
         BlobClient blobClient = containerClient.GetBlobClient(blobItem.Name);
         urls.Add(ReplaceHost(blobClient.Uri.AbsoluteUri));
      }

      return urls;
   }

   public Task<FileResponse> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default)
   {
      throw new NotImplementedException();
   }

   public Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default)
   {
      throw new NotImplementedException();
   }

   public async Task MoveFilesFromTempToEntityContainer(IEnumerable<string> tempContainerNames,
      string containerType,
      Guid id,
      CancellationToken cancellationToken = default)
   {
      BlobContainerClient targetContainerClient = blobServiceClient.GetBlobContainerClient(GetContainerName(containerType, id));

      await targetContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer, cancellationToken: cancellationToken);

      foreach (string tempContainerName in tempContainerNames)
      {
         BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(tempContainerName);

         await foreach (BlobItem blobItem in containerClient.GetBlobsAsync(cancellationToken: cancellationToken))
         {
            BlobClient blobClient = containerClient.GetBlobClient(blobItem.Name);
            BlobClient targetBlobClient = targetContainerClient.GetBlobClient(blobItem.Name);

            CopyFromUriOperation? poller =
               await targetBlobClient.StartCopyFromUriAsync(blobClient.Uri, cancellationToken: cancellationToken);
            await poller.WaitForCompletionAsync(cancellationToken);

            await blobClient.DeleteAsync(DeleteSnapshotsOption.IncludeSnapshots, cancellationToken: cancellationToken);
         }
      }
   }



   private string ReplaceHost(string original, string newHostName = "localhost")
   {
#pragma warning disable IDE0017
      var builder = new UriBuilder(original);
#pragma warning restore IDE0017

      if (builder.Host == "azurite")
      {
         builder.Host = newHostName;
         return builder.Uri.AbsoluteUri;
      }

      return original;
   }
}
