// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data.Common;
using Dapper;
using EventModularMonolith.Modules.Events.Application.Categories.DTOs;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;

namespace EventModularMonolith.Modules.Events.Application.Categories.GetAllCategories;

public sealed class GetAllCategoriesQueryHandler(IDbConnectionFactory dbConnectionFactory) :
     IQueryHandler<GetAllCategoriesQuery, IReadOnlyCollection<CategoryDto>>
{
    public async Task<Result<IReadOnlyCollection<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
             id AS {nameof(CategoryDto.Id)},
             name AS {nameof(CategoryDto.Name)},
             is_archived AS {nameof(CategoryDto.IsArchived)},
             FROM events.categories
             """;

        List<CategoryDto> categories = (await connection.QueryAsync<CategoryDto>(sql, request)).AsList();

        return categories;
    }
}


