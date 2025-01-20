using Azure;

namespace NGrid.Customer.ToReplace.Infrastructure.Search;

public interface IAzureMapsAuthenticationService
{
    public AzureKeyCredential GetKeyCredentials();
}
