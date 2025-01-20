using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using MongoDB.Driver;
using NGrid.Customer.Framework.Templates.Abstraction;
using NGrid.Customer.ToReplace.Infrastructure;
using NGrid.Customer.ToReplace.Tests.Common;
using NGrid.Customer.ToReplace.Tests.Common.TestData;
using NGrid.Framework.Testing.Shared.Integration.Fixtures;
using Xunit;
using Xunit.Abstractions;
using ApiTestUtility = NGrid.Customer.ToReplace.Tests.Common.ApiTestUtility;

namespace NGrid.Customer.ToReplace.Api.IntegrationTests;

public class ToReplaceControllerIntegrationTest : IClassFixture<InMemoryDbContextFixture>
{
    private const string ControllerUrl = "/toReplace";
    private const string CollectionName = "toReplaces";

    private readonly InMemoryDbContextFixture _dbContextFixture;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly WebAppFactory _factory;

    public ToReplaceControllerIntegrationTest(
        InMemoryDbContextFixture dbContextFixture,
        ITestOutputHelper testOutputHelper
    )
    {
        _dbContextFixture = dbContextFixture;
        _testOutputHelper = testOutputHelper;
        var webAppOptions = new DefaultWebAppOptions(
            testOutputHelper,
            new Dictionary<string, bool>(),
            new DatabaseSettings
            {
                ConnectionString = dbContextFixture.ConnectionString,
                DatabaseName = dbContextFixture.DatabaseName,
                ChangeStreamCollectionName = "cs",
            },
            new DatabaseCollectionSettings<ToReplaceDao> { CollectionName = CollectionName }
        );
        _factory = new WebAppFactory(webAppOptions);
    }

    [Fact]
    public async Task InternalPUT_HappyPath()
    {
        var webClient = _factory.CreateDefaultClient();

        var item = ToReplaceFaker.ToReplaceRequestFaker().Generate(1)[0];
        var payload = ApiTestUtility.GetStringContent(item);
        var url = $"{ApiTestUtility.GetRestApiInternalUrl()}{ControllerUrl}/";

        var result = await webClient.SendPutRequestAndCheckResultAsync(
            url,
            payload,
            _testOutputHelper
        );

        var collection = _dbContextFixture.GetCollection<ToReplaceDao>(CollectionName);
        var records1 = (await collection.Find(_ => true).ToListAsync()).ToList();

        var data = records1.Find(MatchByKey(item));

        CheckFields(data, item);
        data.IsActive.Should().BeTrue();
        result.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task InternalPUT_Loader_HappyPath()
    {
        var webClient = _factory.CreateDefaultClient();

        var items = ToReplaceFaker
            .ToReplaceRequestFaker()
            .Generate(10)
            .DistinctBy(v => v.Key)
            .ToList();
        var payload = ApiTestUtility.GetStringContent(items);
        var url = $"{ApiTestUtility.GetRestApiInternalUrl()}{ControllerUrl}/loader";

        var result = await webClient.SendPutRequestAndCheckResultAsync(
            url,
            payload,
            _testOutputHelper
        );

        var collection = _dbContextFixture.GetCollection<ToReplaceDao>(CollectionName);
        var records1 = (await collection.Find(_ => true).ToListAsync()).ToList();

        foreach (var item in items)
        {
            var data = records1.Find(MatchByKey(item));
            CheckFields(data, item);
            data.IsActive.Should().BeTrue();
        }

        ((int)result).ToString().Should().StartWith("20");
    }

    [Fact]
    public async Task InternalGET_HappyPath()
    {
        var webClient = _factory.CreateDefaultClient();

        var item = ToReplaceFaker.ToReplaceRequestFaker().Generate(1).First();
        var payload = ApiTestUtility.GetStringContent(item);
        var url = $"{ApiTestUtility.GetRestApiInternalUrl()}{ControllerUrl}";
        var getUrl = $"{ApiTestUtility.GetRestApiInternalUrl()}{ControllerUrl}/{item.Key}";

        await webClient.SendPutRequestAndCheckResultAsync(url, payload, _testOutputHelper);

        var collection = _dbContextFixture.GetCollection<ToReplaceDao>(CollectionName);
        var records1 = (await collection.Find(_ => true).ToListAsync()).ToList();

        var data = records1.Find(MatchByKey(item));

        data.Should().NotBeNull();
        data!.Description.Should().Be(item.Description);
        data.Key.Should().Be(item.Key);

        var result = await webClient.SendGetRequestAsync(getUrl);

        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task InternalDELETE_HappyPath()
    {
        var webClient = _factory.CreateDefaultClient();

        var item = ToReplaceFaker.ToReplaceRequestFaker().Generate(1).First();
        var payload = ApiTestUtility.GetStringContent(item);
        var url = $"{ApiTestUtility.GetRestApiInternalUrl()}{ControllerUrl}";
        var deleteUrl = $"{ApiTestUtility.GetRestApiInternalUrl()}{ControllerUrl}/{item.Key}";

        await webClient.SendPutRequestAndCheckResultAsync(url, payload, _testOutputHelper);
        var collection = _dbContextFixture.GetCollection<ToReplaceDao>(CollectionName);
        var records1 = (await collection.Find(_ => true).ToListAsync()).ToList();

        var data = records1.Find(MatchByKey(item));

        data.Should().NotBeNull();
        data!.IsActive.Should().BeTrue();

        var result = await webClient.SendDeleteRequestAndCheckResultAsync(
            deleteUrl,
            _testOutputHelper
        );

        records1 = (await collection.Find(_ => true).ToListAsync()).ToList();

        data = records1.Find(MatchByKey(item));
        data.Should().NotBeNull();
        result.Should().Be(HttpStatusCode.OK);
    }

    private static Predicate<ToReplaceDao> MatchByKey(Domain.ToReplace item) =>
        x => x.Key == item.Key;

    private static void CheckFields(ToReplaceDao? data, Domain.ToReplace item)
    {
        data.Should().NotBeNull();
        data!.Description.Should().Be(item.Description);
        data.Key.Should().Be(item.Key);
    }
}