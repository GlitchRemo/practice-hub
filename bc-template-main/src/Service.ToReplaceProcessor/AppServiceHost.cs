using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NGrid.Customer.Framework.Templates.Abstraction.ChangeStream;
using NGrid.Customer.ToReplace.Infrastructure;

namespace NGrid.Customer.ToReplace.Service.ToReplaceProcessor;

public class AppServiceHost(
    IChangeStreamProvider<ToReplaceDao> changeStreamProvider,
    ILogger<AppServiceHost> logger
) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken) =>
        Task.Run(
            async () =>
            {
                try
                {
                    await foreach (
                        var toReplace in changeStreamProvider.GetChangeStream(
                            cancellationToken: stoppingToken
                        )
                    )
                    {
                        if (stoppingToken.IsCancellationRequested)
                            break;

                        if (toReplace == null!)
                            continue;

                        // CODE HERE
                    }
                }
                catch (Exception e)
                {
                    logger.LogError(e, "Failed reading from change stream - {Error}", e.Message);
                    throw;
                }
            },
            stoppingToken
        );
}