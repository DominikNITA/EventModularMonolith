namespace EventModularMonolith.Shared.Application.Storage;

public interface IBlobService
{
   Task<string> UploadAsync(string containerType, Guid id, Stream stream, string contentType, CancellationToken cancellationToken = default);

   Task<IReadOnlyCollection<string>> GetUrlsFromContainerAsync(string containerType, Guid id, CancellationToken cancellationToken = default);

   Task<FileResponse> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default);

   Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default);

   Task MoveFilesFromTempToEntityContainer(IEnumerable<string> tempContainerNames,
      string containerType,
      Guid id,
      CancellationToken cancellationToken = default);
}

public static class ContainerTags
{
   public const string Venue = "venue";
   public const string Speaker = "speaker";
   public const string Event = "event";
   public const string Temporary = "temp";
}
