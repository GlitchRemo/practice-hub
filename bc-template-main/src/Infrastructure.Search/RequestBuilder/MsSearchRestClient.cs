using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using Microsoft.Extensions.Logging;
using NGrid.Customer.Framework.Shared.Exceptions;
using NGrid.Customer.ToReplace.Domain.Maps;

namespace NGrid.Customer.ToReplace.Infrastructure.Search.RequestBuilder;

[ExcludeFromCodeCoverage]
public class MsSearchRestClient
{
    private readonly HttpPipeline _pipeline;
    private readonly Uri _endpoint;
    private readonly string _clientId;
    private readonly string _apiVersion;
    private readonly ILogger<AzureSearchClient> _logger;

    /// <summary> Initializes a new instance of SearchRestClient. </summary>
    /// <param name="pipeline"> The HTTP pipeline for sending and receiving REST requests and responses. </param>
    /// <param name="endpoint"> server parameter. </param>
    /// <param name="clientId"> Specifies which account is intended for usage in conjunction with the Azure AD security model.  It represents a unique ID for the Azure Maps account and can be retrieved from the Azure Maps management  plane Account API. To use Azure AD security in Azure Maps see the following [articles](https://aka.ms/amauthdetails) for guidance. </param>
    /// <param name="apiVersion"> Api Version. </param>
    public MsSearchRestClient(HttpPipeline pipeline,
        ILogger<AzureSearchClient> logger,
        Uri endpoint = null,
        string clientId = null,
        string apiVersion = "1.0")
    {
        _pipeline = pipeline ?? throw new ArgumentNullException(nameof(pipeline));
        _endpoint = endpoint ?? new Uri("https://atlas.microsoft.com");
        _clientId = clientId;
        _apiVersion = apiVersion ?? throw new ArgumentNullException(nameof(apiVersion));
        _logger = logger;
    }

    private HttpMessage CreateFuzzySearchRequest(string query, bool? isTypeAhead, int? top, int? skip,
        IEnumerable<int> categoryFilter, IEnumerable<string> countryFilter, double? lat, double? lon,
        int? radiusInToReplaces, string topLeft, string btmRight, string language,
        IEnumerable<SearchIndex> extendedPostalCodesFor, int? minFuzzyLevel, int? maxFuzzyLevel,
        IEnumerable<SearchIndex> indexFilter, IEnumerable<string> brandFilter,
        IEnumerable<ElectricVehicleConnector> electricVehicleConnectorFilter, GeographicEntity? entityType,
        LocalizedMapView? localizedMapView, OperatingHoursRange? operatingHours)
    {
        var message = _pipeline.CreateMessage();
        var request = message.Request;
        request.Method = RequestMethod.Get;
        var uri = new MsRawRequestUriBuilder();
        uri.Reset(_endpoint);
        uri.AppendPath("/search/fuzzy/", false);
        uri.AppendPath("json", true);
        uri.AppendQuery("api-version", _apiVersion, true);
        uri.AppendQuery("query", query, true);
        if (isTypeAhead != null)
        {
            uri.AppendQuery("typeahead", isTypeAhead.Value.ToString().ToLower(), true);
        }

        if (top != null)
        {
            uri.AppendQuery("limit", top.Value.ToString(), true);
        }

        if (skip != null)
        {
            uri.AppendQuery("ofs", skip.Value.ToString(), true);
        }

        if (categoryFilter?.Any() == true)
        {
            uri.AppendQueryDelimited("categorySet", categoryFilter, ",", true);
        }

        if (countryFilter?.Any() == true)
        {
            uri.AppendQueryDelimited("countrySet", countryFilter, ",", true);
        }

        if (lat != null)
        {
            uri.AppendQuery("lat", lat.Value.ToString(), true);
        }

        if (lon != null)
        {
            uri.AppendQuery("lon", lon.Value.ToString(), true);
        }

        if (radiusInToReplaces != null)
        {
            uri.AppendQuery("radius", radiusInToReplaces.Value.ToString(), true);
        }

        if (topLeft != null)
        {
            uri.AppendQuery("topLeft", topLeft, true);
        }

        if (btmRight != null)
        {
            uri.AppendQuery("btmRight", btmRight, true);
        }

        if (language != null)
        {
            uri.AppendQuery("language", language, true);
        }

        if (extendedPostalCodesFor?.Any() == true)
        {
            uri.AppendQueryDelimited("extendedPostalCodesFor", extendedPostalCodesFor, ",", true);
        }

        if (minFuzzyLevel != null)
        {
            uri.AppendQuery("minFuzzyLevel", minFuzzyLevel.Value.ToString(), true);
        }

        if (maxFuzzyLevel != null)
        {
            uri.AppendQuery("maxFuzzyLevel", maxFuzzyLevel.Value.ToString(), true);
        }

        if (indexFilter?.Any() == true)
        {
            uri.AppendQueryDelimited("idxSet", indexFilter, ",", true);
        }

        if (brandFilter?.Any() == true)
        {
            uri.AppendQueryDelimited("brandSet", brandFilter, ",", true);
        }

        if (electricVehicleConnectorFilter?.Any() == true)
        {
            uri.AppendQueryDelimited("connectorSet", electricVehicleConnectorFilter, ",", true);
        }

        if (entityType != null)
        {
            uri.AppendQuery("entityType", entityType.Value.ToString(), true);
        }

        if (localizedMapView != null)
        {
            uri.AppendQuery("view", localizedMapView.Value.ToString(), true);
        }

        if (operatingHours != null)
        {
            uri.AppendQuery("openingHours", operatingHours.Value.ToString(), true);
        }

        request.Uri = uri;
        if (_clientId != null)
        {
            request.Headers.Add("x-ms-client-id", _clientId);
        }

        request.Headers.Add("Accept", "application/json");
        return message;
    }

    public async Task<Response<string>> FuzzySearchAsync(string query, FuzzySearchOptions options,
        CancellationToken cancellationToken = default)
    {
        if (query == null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        using var message = CreateFuzzySearchRequest(query, options?.IsTypeAhead, options?.Top, options?.Skip,
            options?.CategoryFilter, options?.CountryFilter, options?.Coordinates?.Latitude,
            options?.Coordinates?.Longitude, options?.RadiusInToReplaces,
            options?.BoundingBox != null ? options.BoundingBox.North + "," + options.BoundingBox.West : null,
            options?.BoundingBox != null ? options.BoundingBox.South + "," + options.BoundingBox.East : null,
            options?.Language.ToString(), options?.ExtendedPostalCodesFor, options?.MinFuzzyLevel,
            options?.MaxFuzzyLevel, options?.IndexFilter, options?.BrandFilter,
            options?.ElectricVehicleConnectorFilter, options?.EntityType, options?.LocalizedMapView,
            options?.OperatingHours);

        _logger.LogInformation("Making request to Azure API with URI: {Uri}",
            message.Request.Uri);
        await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);

        switch (message.Response.Status)
        {
            case 200:
            {
                _logger.LogInformation("Azure Maps API returned successful response.");

                StreamReader reader = new StreamReader(message.Response.ContentStream);
                string json = await reader.ReadToEndAsync().ConfigureAwait(false);

                return Response.FromValue(json, message.Response);
            }
            case 401:
            {
                _logger.LogInformation("Azure Maps API returned unauthorized response.");
                StreamReader reader = new StreamReader(message.Response.ContentStream);
                string json = await reader.ReadToEndAsync().ConfigureAwait(false);

                return Response.FromValue(json, message.Response);
            }
            case 404:
                _logger.LogError("Azure Maps API not found at URI: {Uri}", message.Request.Uri);
                throw new NotFoundException("Azure Maps API not found at: " + message.Request.Uri);
            default:
                _logger.LogError("Azure Maps API returned error message {Response}", message.Response);
                throw new RequestFailedException(message.Response);
        }
    }
}
