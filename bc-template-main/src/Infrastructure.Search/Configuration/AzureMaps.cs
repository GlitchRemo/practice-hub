using System.Diagnostics.CodeAnalysis;

namespace NGrid.Customer.ToReplace.Infrastructure.Search.Configuration;

[ExcludeFromCodeCoverage]
public class AzureMaps : IAzureMapsSettings
{
    public string SasKey { get; set; }
    public string BaseUrl { get; set; }
}
