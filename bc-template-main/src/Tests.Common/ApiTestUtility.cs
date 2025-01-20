using System.Net;
using System.Text;
using Newtonsoft.Json;
using NGrid.Customer.Framework.Shared;
using NGrid.Customer.ToReplace.Api;
using NGrid.Customer.ToReplace.Tests.Common.TestData;
using Xunit.Abstractions;

namespace NGrid.Customer.ToReplace.Tests.Common;

public static class ApiTestUtility
{
    public static string GetRestApiInternalUrl()
        => ApiTestConstants.AppUrl.Trim('/') + AppConstants.ApiInternalPath + "/" + AppConstants.ToReplaceApiPath;

    public static string GetRestApiInternalPath()
        => AppConstants.ApiInternalPath + "/" + AppConstants.ToReplaceApiPath;

    public static string GetRestApiToReplaceUrl()
        => ApiTestConstants.AppUrl.Trim('/');

    public static string ConvertToJson(object obj)
        => JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });


    public static StringContent GetStringContent(object obj)
        => new(ConvertToJson(obj), Encoding.UTF8, "application/json");

    public static Task<HttpResponseMessage> SendGetRequestAsync(this HttpClient client, string appUrl, string graphQlPath = "")
    {
        var url = appUrl.Trim('/') + graphQlPath;
        client.DefaultRequestHeaders.Add(SystemConstants.EventType,"type");
        client.DefaultRequestHeaders.Add(SystemConstants.Source,"source");
        return client.GetAsync(url);
    }

    public static Task<HttpResponseMessage> SendPostRequestAsync(this HttpClient client, StringContent content, string appUrl, string graphQlPath = "")
    {
        content.Headers.Add(SystemConstants.EventType,"type");
        content.Headers.Add(SystemConstants.Source,"source");
        var url = appUrl.Trim('/')  + graphQlPath;
        return client.PostAsync(url, content);
    }

    public static Task<HttpResponseMessage> SendPutRequestAsync(this HttpClient client, StringContent content, string appUrl, string graphQlPath = "")
    {
        content.Headers.Add(SystemConstants.EventType,"type");
        content.Headers.Add(SystemConstants.Source,"source");
        var url = appUrl.Trim('/') + graphQlPath;
        return client.PutAsync(url, content);
    }

    public static Task<HttpResponseMessage> SendDeleteRequestAsync(this HttpClient client, string appUrl, string graphQlPath = "")
    {
        var url = appUrl.Trim('/') + graphQlPath;
        client.DefaultRequestHeaders.Add(SystemConstants.EventType,"type");
        client.DefaultRequestHeaders.Add(SystemConstants.Source,"source");
        return client.DeleteAsync(url);
    }

    public static async Task<HttpStatusCode> SendPutRequestAndCheckResultAsync(this HttpClient client,
        string url, StringContent payload, ITestOutputHelper testOutputHelper, HttpStatusCode resultCodeToCheck = HttpStatusCode.OK)
    {
        HttpStatusCode resultCode;
        using (var cts = new CancellationTokenSource())
        {
            cts.CancelAfter(TimeSpan.FromSeconds(10));
            do
            {
                var http = await client.SendPutRequestAsync(payload, url);
                resultCode = http.StatusCode;
                var resultString = await http.Content.ReadAsStringAsync();
                testOutputHelper.WriteLine($"Response Status={resultCode}");
                testOutputHelper.WriteLine(resultString);
            } while (resultCode != resultCodeToCheck && !cts.IsCancellationRequested);
        }
        return resultCode;
    }

    public static async Task<HttpStatusCode> SendDeleteRequestAndCheckResultAsync(this HttpClient client,
        string url, ITestOutputHelper testOutputHelper,  HttpStatusCode resultCodeToCheck = HttpStatusCode.OK)
    {
        HttpStatusCode resultCode;
        using (var cts = new CancellationTokenSource())
        {
            cts.CancelAfter(TimeSpan.FromSeconds(10));
            do
            {
                var http = await client.SendDeleteRequestAsync(url);
                resultCode = http.StatusCode;
                var resultString = await http.Content.ReadAsStringAsync();
                testOutputHelper.WriteLine($"Response Status={resultCode}");
                testOutputHelper.WriteLine(resultString);
            } while (resultCode != resultCodeToCheck && !cts.IsCancellationRequested);
        }
        return resultCode;
    }
}