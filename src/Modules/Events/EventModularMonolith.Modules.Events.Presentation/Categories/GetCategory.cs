// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.Categories.GetCategory;
using EventModularMonolith.Modules.Events.Application.Categories.DTOs;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using EventModularMonolith.Modules.Events.Application.Categories.GetAllCategories;
using EventModularMonolith.Shared.Application.Caching;
using System.Reflection;

namespace EventModularMonolith.Modules.Events.Presentation.Categories;
internal sealed class GetCategory : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapGet("categories/{id}", async (Guid id, ISender sender, ICacheService cacheService) => await CacheHelper.QueryWithCache($"getCategory-{id}", cacheService, sender, new GetCategoryQuery(id)))
         .RequireAuthorization()
         .Produces<CategoryDto>()
         .WithTags(Tags.Categories)
         .WithName("GetCategory");

   }
}
