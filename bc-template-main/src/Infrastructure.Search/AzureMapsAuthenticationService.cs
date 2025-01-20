using System.Diagnostics.CodeAnalysis;
using Azure;
using NGrid.Customer.ToReplace.Infrastructure.Search.Configuration;

namespace NGrid.Customer.ToReplace.Infrastructure.Search;

[ExcludeFromCodeCoverage]
public class AzureMapsAuthenticationService : IAzureMapsAuthenticationService
{
    private readonly IAzureMapsSettings _settings;
    private AzureSasCredential _sasCredential;

    public AzureMapsAuthenticationService(IAzureMapsSettings settings)
    {
        _settings = settings;
    }

    public AzureKeyCredential GetKeyCredentials()
    {
        AzureKeyCredential credential = new AzureKeyCredential(_settings.SasKey);

        return credential;
    }
}
