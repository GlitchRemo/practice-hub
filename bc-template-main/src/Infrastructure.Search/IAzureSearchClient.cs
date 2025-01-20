using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using NGrid.Customer.ToReplace.Domain.AddressSearch;

namespace NGrid.Customer.ToReplace.Infrastructure.Search;

public interface IAzureSearchClient
{
    Task<Response<string>> FuzzySearchAsJsonAsync(string query, IEnumerable<int> categorySet = null,
        List<string> countrySet = null, decimal? lat = null,
        decimal? lon = null, int? radius = null, string topLeft = null,
        string btmRight = null, string language = null, string extendedPostalCodesFor = null,
        List<IdxSet> idxSet = null, List<string> brandSet = null, List<ConnectorSet> connectorSet = null,
        string view = null, string openingHours = null,
        int? minFuzzyLevel = null, int? maxFuzzyLevel = null,
        int? limit = null, int? ofs = null,
        bool? typeahead = null,
        CancellationToken cancellationToken = default);
}
