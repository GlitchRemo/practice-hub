using NGrid.Customer.Framework.Templates.Abstraction;
using NGrid.Customer.ToReplace.Api;
using NGrid.Customer.ToReplace.Infrastructure;
using NGrid.Framework.Testing.Shared.Integration;
using Xunit.Abstractions;

namespace NGrid.Customer.ToReplace.Tests.Common;

public class WebAppFactory(DefaultWebAppOptions options)
    : LoggingWebAppFactory<Program, DefaultWebAppOptions>(options)
{
    protected override IEnumerable<KeyValuePair<string, string?>> CreateConfiguration()
    {
        return new Dictionary<string, string>
        {
            ["DatabaseSettings:ConnectionString"] = Options.DatabaseSettings.ConnectionString,
            ["DatabaseSettings:DatabaseName"] = Options.DatabaseSettings.DatabaseName,
            ["DatabaseSettings:MaxConnectionPoolSize"] = "100",
            ["DatabaseSettings:MaxConnecting"] = "2",
            ["DatabaseSettings:ChangeStreamCollectionName"] = Options.DatabaseSettings.ChangeStreamCollectionName,
            ["Collection_ToReplaceDao:CollectionName"] = Options.CollectionSettings.CollectionName,
            ["DatabaseSettings:SkipDatabaseValidation"] = "true"
        }!;
    }
}

public record DefaultWebAppOptions : WebAppFactoryOptions
{
    public Dictionary<string, bool> Toggles { get; }
    public DatabaseSettings DatabaseSettings { get; }
    public DatabaseCollectionSettings<ToReplaceDao> CollectionSettings { get; }

    public DefaultWebAppOptions(
        ITestOutputHelper TestOutput,
        Dictionary<string, bool> toggles,
        DatabaseSettings databaseSettings,
        DatabaseCollectionSettings<ToReplaceDao> collectionSettings,
        string? TestUrl = null
    )
        : base(TestOutput, TestUrl)
    {
        Toggles = toggles;
        DatabaseSettings = databaseSettings;
        CollectionSettings = collectionSettings;
    }
}