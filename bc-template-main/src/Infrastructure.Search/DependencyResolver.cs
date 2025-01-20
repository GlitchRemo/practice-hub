using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NGrid.Customer.ToReplace.Domain.Repository;
using NGrid.Customer.ToReplace.Infrastructure.Search.Configuration;

namespace NGrid.Customer.ToReplace.Infrastructure.Search;

public static class DependencyResolver
{
    public static IServiceCollection AddApiInfrastructureSearchDependencyResolver(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AzureMaps>(configuration.GetSection("AzureMaps"));
        services.AddSingleton<ISearchRepository, AzureSearchRepository>();
        services.AddSingleton<IAzureMapsAuthenticationService, AzureMapsAuthenticationService>();
        services.AddSingleton<IAzureSearchClient, AzureSearchClient>();
        return services;
    }

}