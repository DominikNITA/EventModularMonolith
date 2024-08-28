using System.Buffers;
using System.Text.Json;
using EventModularMonolith.Shared.Application.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace EventModularMonolith.Shared.Infrastructure.Caching;

internal sealed class CacheService(IDistributedCache cache) : ICacheService
{

   private static JsonSerializerOptions _options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
   public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
   {
      byte[]? bytes = await cache.GetAsync(key, cancellationToken);

      return bytes is null ? default : Deserialize<T>(bytes);
   }

   public Task SetAsync<T>(
      string key,
      T value,
      TimeSpan? expiration = null,
      CancellationToken cancellationToken = default)
   {
      byte[] bytes = Serialize(value);

      return cache.SetAsync(key, bytes, CacheOptions.Create(expiration), cancellationToken);
   }

   public Task RemoveAsync(string key, CancellationToken cancellationToken = default) =>
      cache.RemoveAsync(key, cancellationToken);

   private static T Deserialize<T>(byte[] bytes)
   {
      return JsonSerializer.Deserialize<T>(bytes, _options)!;
   }

   private static byte[] Serialize<T>(T value)
   {
      var buffer = new ArrayBufferWriter<byte>();
      using var writer = new Utf8JsonWriter(buffer);
      JsonSerializer.Serialize(writer, value, _options);
      return buffer.WrittenSpan.ToArray();
   }
}
