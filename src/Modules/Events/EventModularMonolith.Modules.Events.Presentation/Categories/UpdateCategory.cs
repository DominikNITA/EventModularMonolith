// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using EventModularMonolith.Modules.Events.Application.Categories.UpdateCategory;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Shared.Presentation;
using EventModularMonolith.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventModularMonolith.Modules.Events.Presentation.Categories;

internal sealed class UpdateCategory : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapPut("categories/{id}", async (Guid id,UpdateCategoryRequest request, ISender sender) =>
         {
            var command = new UpdateCategoryCommand(
               id,
               request.Name
            );

            Result result = await sender.Send(command);

            return result.Match(() => Results.Ok(), ApiResults.Problem);
         })
      .WithTags(Tags.Categories);
   }

   internal sealed class UpdateCategoryRequest
   {
      public string Name { get; set; } = string.Empty;
   }
}
