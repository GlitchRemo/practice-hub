using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NGrid.Customer.Framework.Templates.DataBase.Repository;

namespace NGrid.Customer.ToReplace.Infrastructure;

public static class DependencyResolver
{
    public static IServiceCollection AddInfrastructureDependencyResolver(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddMongoDbDependencyResolver(configuration)
            .AddDaoDependencyResolver<Domain.ToReplace, ToReplaceDao, int>(configuration, true);
    }
}