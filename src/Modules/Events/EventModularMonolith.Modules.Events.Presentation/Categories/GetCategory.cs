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

namespace EventModularMonolith.Modules.Events.Presentation.Categories;

internal sealed class GetCategory : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapGet("categories/{id}", async (Guid id, ISender sender) =>
         {
            Result<CategoryDto> result = await sender.Send(new GetCategoryQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
         })
         .RequireAuthorization()
         .WithTags(Tags.Categories);
   }
}
