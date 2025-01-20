using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NGrid.Customer.Framework.Templates.Abstraction;
using NGrid.Customer.ToReplace.Domain.AddressSearch;
using NGrid.Customer.ToReplace.Domain.Repository;

namespace NGrid.Customer.ToReplace.Infrastructure.Search;

[ExcludeFromCodeCoverage]
public class AzureSearchRepository : ISearchRepository
{
    private readonly ILogger<AzureSearchRepository> _logger;
    private readonly IAzureSearchClient _client;
    private readonly IAuditTrailService _audit;

    public AzureSearchRepository(
        ILogger<AzureSearchRepository> logger,
        IAzureSearchClient client,
        IAuditTrailService audit)
    {
        _logger = logger;
        _client = client;
        _audit = audit;
    }
    
    public async Task<string> SearchAsJsonAsync(string query, IEnumerable<int> categorySet = null,
        List<string> countrySet = null, decimal? lat = null,
        decimal? lon = null, int? radius = null, string topLeft = null,
        string btmRight = null, string language = null, string extendedPostalCodesFor = null,
        List<IdxSet> idxSet = null, List<string> brandSet = null, List<ConnectorSet> connectorSet = null,
        string view = null, string openingHours = null,
        int? minFuzzyLevel = null, int? maxFuzzyLevel = null,
        int? limit = null, int? ofs = null,
        bool? typeahead = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
            throw new ArgumentNullException(nameof(query));

        try
        {
            var message =
                await _client.FuzzySearchAsJsonAsync(query, categorySet, countrySet, lat, lon, radius,
                    topLeft, btmRight, language, extendedPostalCodesFor, idxSet, brandSet, connectorSet,
                    view, openingHours, minFuzzyLevel, maxFuzzyLevel, limit, ofs, typeahead, cancellationToken);
            
            return message.HasValue 
                ? message.Value
                : string.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError("{CorrelationId} | Operation 'SearchAsJsonAsync' failed with error : {Error}", _audit.GetCorrelationId(), e.Message);
            throw;
        }
    }
}
