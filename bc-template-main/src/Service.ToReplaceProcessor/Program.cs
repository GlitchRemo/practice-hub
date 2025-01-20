using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGrid.Customer.Framework.Shared;
using NGrid.Customer.Framework.Templates.DataBase.Repository;
using NGrid.Customer.ToReplace.Infrastructure;
using NGrid.Customer.ToReplace.Service.ToReplaceProcessor;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
Log.Information($"{AppConstants.ToReplaceProcessorServiceName} Starting...");

try
{
    var builder = Host.CreateApplicationBuilder(args);
    builder.ConfigureApplicationLogger();

    builder.Services.AddInfrastructureDependencyResolver(builder.Configuration);
    builder.Services.AddChangeStreamService<ToReplaceDao>();
    builder.Services.AddHostedService<AppServiceHost>();
    var host = builder.Build();

    await host.RunAsync();

    Log.Information($"{AppConstants.ToReplaceProcessorServiceName} Stopped...");
}
catch (Exception ex)
{
    Log.Fatal(ex, $"An unhandled exception occured during {AppConstants.ToReplaceProcessorServiceName} bootstrapping....");
}
finally
{
    Log.CloseAndFlush();
}