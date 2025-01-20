using System;
using Microsoft.AspNetCore.Builder;
using NGrid.Customer.Framework.Shared;
using NGrid.Customer.ToReplace.Api;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
Log.Information($"{AppConstants.ToReplaceApiName} Starting...");

try
{
    var hostBuilder = WebApplication.CreateBuilder(args);
    hostBuilder.ConfigureApplicationLogger();
    var startup = new Startup(hostBuilder.Configuration);
    startup.ConfigureServices(hostBuilder.Services);
    var host = hostBuilder.Build();
    startup.Configure(host, host.Environment);
    host.Run();
    Log.Information($"{AppConstants.ToReplaceApiName} Stopped...");
}
catch (Exception ex)
{
    Log.Fatal(
        ex,
        $"An unhandled exception occured during {AppConstants.ToReplaceApiName} bootstrapping...."
    );
}
finally
{
    Log.CloseAndFlush();
}

namespace NGrid.Customer.ToReplace.Api
{
    public partial class Program;
}