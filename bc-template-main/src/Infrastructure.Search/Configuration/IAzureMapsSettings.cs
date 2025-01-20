namespace NGrid.Customer.ToReplace.Infrastructure.Search.Configuration;

public interface IAzureMapsSettings
{
    string SasKey { get; set; }
    public string BaseUrl { get; set; }
}
