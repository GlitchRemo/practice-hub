using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core.GeoJson;
using Azure.Core.Pipeline;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using NGrid.Customer.Framework.Shared.Exceptions;
using NGrid.Customer.ToReplace.Domain.AddressSearch;
using NGrid.Customer.ToReplace.Domain.Maps;
using NGrid.Customer.ToReplace.Infrastructure.FeatureManagement;
using NGrid.Customer.ToReplace.Infrastructure.Search.Configuration;
using NGrid.Customer.ToReplace.Infrastructure.Search.RequestBuilder;

namespace NGrid.Customer.ToReplace.Infrastructure.Search;

[ExcludeFromCodeCoverage]
public class AzureSearchClient : IAzureSearchClient
{
    private const string ErrorWhileConfiguringTheMssearchrestclientMissingBaseUrl =
        "Error while configuring the MsSearchRestClient. Missing base url.";

    private readonly IAzureMapsSettings _settings;
    private readonly IAzureMapsAuthenticationService _authenticationService;
    private readonly ILogger<AzureSearchClient> _logger;
    private readonly IFeatureManager _featureManager;

    public AzureSearchClient(IAzureMapsSettings settings,
        ILogger<AzureSearchClient> logger, IFeatureManager featureManager,
        IAzureMapsAuthenticationService authenticationService)
    {
        _logger = logger;
        _featureManager = featureManager;
        _settings = settings;
        _authenticationService = authenticationService;
    }

    private FuzzySearchOptions GetOptions(
        IEnumerable<int> categorySet = null, List<string> countrySet = null, decimal? lat = null,
        decimal? lon = null, int? radius = null, string topLeft = null,
        string btmRight = null, string language = null, string extendedPostalCodesFor = null,
        List<IdxSet> idxSet = null, List<string> brandSet = null, List<ConnectorSet> connectorSet = null,
        string view = null, string openingHours = null,
        int? minFuzzyLevel = null, int? maxFuzzyLevel = null,
        int? limit = null, int? ofs = null,
        bool? typeahead = null)
    {
        var west = topLeft is not null ? double.Parse(topLeft.Split(",")[0]) : (double?)null;
        var south = btmRight is not null ? double.Parse(btmRight.Split(",")[0]) : (double?)null;
        var east = btmRight is not null ? double.Parse(btmRight.Split(",")[1]) : (double?)null;
        var north = topLeft is not null ? double.Parse(topLeft.Split(",")[1]) : (double?)null;

        try
        {
            var result = new FuzzySearchOptions
            {
                CategoryFilter = categorySet,
                CountryFilter = countrySet,
                Coordinates = lon is not null && lat is not null ? new GeoPosition((double)lon, (double)lat) : null,
                RadiusInToReplaces = radius,
                BoundingBox = west is not null && south is not null && east is not null && north is not null
                    ? new GeoBoundingBox(west.Value, south.Value, east.Value, north.Value)
                    : null,
                ExtendedPostalCodesFor = extendedPostalCodesFor?.Split(",")
                    .Select(x => new SearchIndex(x)),
                IndexFilter = idxSet?.Select(x => new SearchIndex(x.ToString())),
                BrandFilter = brandSet,
                ElectricVehicleConnectorFilter = connectorSet?.Select(x =>
                    new ElectricVehicleConnector(x.ToString())),
                MinFuzzyLevel = minFuzzyLevel,
                MaxFuzzyLevel = maxFuzzyLevel,
                Skip = ofs,
                Top = limit,
                IsTypeAhead = typeahead
            };
            if (language is not null)
            {
                result.Language = new SearchLanguage(language);
            }

            if (view is not null)
            {
                result.LocalizedMapView = new LocalizedMapView(view);
            }

            if (openingHours is not null)
            {
                result.OperatingHours = new OperatingHoursRange(openingHours);
            }

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError("Error while parsing fuzzy search options: {message}", e.Message);
            throw;
        }
    }

    public Task<Response<string>> FuzzySearchAsJsonAsync(string query, IEnumerable<int> categorySet = null,
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
        var options = GetOptions(categorySet, countrySet, lat, lon, radius, topLeft, btmRight, language,
            extendedPostalCodesFor,
            idxSet, brandSet, connectorSet, view, openingHours, minFuzzyLevel, maxFuzzyLevel, limit, ofs,
            typeahead);
        try
        {
            var retryAttempts = 0;
            var response = AttemptRequest(query, options, cancellationToken, retryAttempts).Result;
            if (response.GetRawResponse().Status == 401)
            {
                throw new HttpException(HttpStatusCode.Unauthorized, response.Value);
            }

            return Task.FromResult(response);
        }
        catch (Exception e)
        {
            _logger.LogError("Error when making request to Azure Maps API: {message}", e.Message);
            throw;
        }
    }

    private Task<Response<string>> AttemptRequest(string query, FuzzySearchOptions options,
        CancellationToken cancellationToken, int retryAttempts)
    {
        bool requestNewCredentials = retryAttempts > 0;
        if (requestNewCredentials)
        {
            _logger.LogInformation("Requesting new SAS token. Attempt #{retryAttempts}.", retryAttempts);
        }

        var response = CreateMsSearchClient(requestNewCredentials).FuzzySearchAsync(query,
            options, cancellationToken);
        if (response.Result.Value.Contains("Invalid SAS access token") && retryAttempts < 3)
        {
            return AttemptRequest(query, options, cancellationToken, ++retryAttempts);
        }

        return response;
    }

    private MsSearchRestClient CreateMsSearchClient(bool requestNewCredentials = false)
    {
        if (string.IsNullOrEmpty(_settings.BaseUrl))
        {
            _logger.LogError(ErrorWhileConfiguringTheMssearchrestclientMissingBaseUrl);
            throw new ConfigurationException(ErrorWhileConfiguringTheMssearchrestclientMissingBaseUrl);
        }

        var endpoint = new Uri(_settings.BaseUrl);
        var options = new MapsSearchClientOptions();
        HttpPipeline pipeline;

        var auth = _authenticationService.GetKeyCredentials();
        _logger.LogInformation("Creating custom MsSearchRestClient using GetKeyCredentials");
        pipeline = HttpPipelineBuilder.Build(options, new MsAzureKeyCredentialPolicy(auth, "subscription-key"));

        return new MsSearchRestClient(pipeline, _logger, endpoint);
    }
}
