// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data.Common;
using Dapper;
using EventModularMonolith.Modules.Events.Application.Categories.DTOs;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;
using EventModularMonolith.Modules.Events.Domain.Categories;

namespace EventModularMonolith.Modules.Events.Application.Categories.GetCategory;

public sealed class GetCategoryQueryHandler(IDbConnectionFactory dbConnectionFactory) :
     IQueryHandler<GetCategoryQuery, CategoryDto>
{
    public async Task<Result<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(CategoryDto.Id)},
                 name AS {nameof(CategoryDto.Name)},
                 is_archived AS {nameof(CategoryDto.IsArchived)},
             FROM events.categories
             WHERE id = @CategoryId
             """;

        CategoryDto? category = await connection.QuerySingleOrDefaultAsync<CategoryDto>(sql, request);

        if (category is null)
        {
            return Result.Failure<CategoryDto>(CategoryErrors.NotFound(request.CategoryId));
        }

        return category;
    }
}
