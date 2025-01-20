using System;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using NGrid.Customer.Framework.Templates.Abstraction;
using NGrid.Customer.Framework.WebApi.Exceptions;
using NGrid.Customer.ToReplace.Infrastructure;

namespace NGrid.Customer.ToReplace.Api.GraphQL;

public class ToReplaceQuery
{
    [GraphQLName("toReplace")]
    [UsePaging(IncludeTotalCount = false, DefaultPageSize = 10, MaxPageSize = 1000)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType(typeof(ListType<NonNullType<ObjectType<ToReplaceDao>>>))]
    public IExecutable<ToReplaceDao> GetToReplaces(
        [Service] IDataBaseContext<ToReplaceDao> repository,
        [Service] ILogger<ToReplaceQuery> logger
    )
    {
        try
        {
            return repository.Collection.Find(m => m.IsActive).AsExecutable();
        }
        catch (Exception e)
        {
            logger.LogError("Error when executing GraphQL query: {Error}", e);
            throw new BadRequestException(e.Message);
        }
    }
}