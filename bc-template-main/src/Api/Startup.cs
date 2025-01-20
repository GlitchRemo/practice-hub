using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using NGrid.Customer.Framework.WebApi;
using NGrid.Customer.ToReplace.Api.GraphQL;
using NGrid.Customer.ToReplace.Application;
using NGrid.Customer.ToReplace.Infrastructure;

namespace NGrid.Customer.ToReplace.Api;

public class Startup(IConfiguration configuration) : BaseStartup(configuration)
{
    public override string ApiName => "SetMe";
    public override string ApiShortName => "SetMe";
    protected override bool AllowSynchronousIO => true;

    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.AddSingleton<IValidator<Domain.ToReplace>, ToReplaceRequestValidator>();
        services.AddInfrastructureDependencyResolver(Configuration);
    }

    protected override void AddGraphQlServer(IServiceCollection services)
    {
        services
            .AddGraphQlStitchingWithMongoIntegration()
            .AddQueryType<ToReplaceQuery>();
    }
}