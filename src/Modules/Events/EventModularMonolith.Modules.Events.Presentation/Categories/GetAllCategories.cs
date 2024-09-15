// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.Categories.GetAllCategories;
using EventModularMonolith.Modules.Events.Application.Categories.DTOs;
using EventModularMonolith.Shared.Application.Caching;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Events.Presentation.Categories;

internal sealed class GetAllCategories : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapGet("categories", async (ISender sender, ICacheService cacheService) => await CacheHelper.QueryWithCache($"getCategories", cacheService, sender, new GetAllCategoriesQuery()))
         .Produces<Result<IReadOnlyCollection<CategoryDto>>>()
         .WithTags(Tags.Categories)
         .WithName("GetCategories");
   }
}
