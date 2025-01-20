using System.Diagnostics.CodeAnalysis;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;

namespace NGrid.Customer.ToReplace.Infrastructure.Search.RequestBuilder;

/// <summary> The MapsSasCredentialPolicy used for SAS authentication. </summary>
[ExcludeFromCodeCoverage]
internal sealed class MsMapsSasCredentialPolicy : HttpPipelineSynchronousPolicy
{
    private readonly string _sasAuthenticationHeader = "Authorization";
    private readonly string _sasPrefix = "jwt-sas";
    private readonly AzureSasCredential _credential;

    /// <summary>
    /// Initializes a new instance of the <see cref="MsMapsSasCredentialPolicy"/> class.
    /// </summary>
    /// <param name="credential">The <see cref="AzureSasCredential"/> used to authenticate requests.</param>
    public MsMapsSasCredentialPolicy(AzureSasCredential credential)
    {
        _credential = credential;
    }

    /// <inheritdoc/>
    public override void OnSendingRequest(HttpMessage message)
    {
        base.OnSendingRequest(message);
        message.Request.Headers.SetValue(_sasAuthenticationHeader, _sasPrefix + " " + _credential.Signature);
    }
}
