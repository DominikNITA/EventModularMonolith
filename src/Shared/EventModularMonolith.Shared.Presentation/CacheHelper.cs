using EventModularMonolith.Shared.Application.Caching;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EventModularMonolith.Shared.Presentation;

public static class CacheHelper
{
   public static async Task<IResult> QueryWithCache<T>(string cacheKey, ICacheService cacheService, ISender sender, IQuery<T> query)
   {
      T response = await cacheService.GetAsync<T>(cacheKey);

      if (response is not null)
      {
         return Results.Ok<T>(response);
      }

      Result<T> result = await sender.Send(query);

      if (result.IsSuccess)
      {
         await cacheService.SetAsync(cacheKey, result.Value);
      }

      return result.Match(Results.Ok, ApiResults.Problem);
   }
}
