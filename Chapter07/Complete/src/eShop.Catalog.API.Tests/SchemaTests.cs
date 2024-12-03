using CookieCrumble;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ISession = eShop.Catalog.Sessions.ISession;

namespace eShop.Catalog.API.Tests;

public class SchemaTests
{
    [Fact(Skip = "Do not need this one.")]
    public async Task SchemaChanged()
    {
        var schema = 
            await new ServiceCollection()
                .AddApplicationServices()
                .AddGraphQL()
                .AddCatalogTypes()
                .AddGraphQLConventions()
                .AddParameterExpressionBuilder<HttpContext>(ctx => default!)
                .BuildSchemaAsync();
        
        schema.MatchSnapshot();
    }
}