using System;
namespace OpenProvider
{
    public interface IOpenProviderHttpClient : IDisposable
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption responseHeadersRead,
           CancellationToken cancellationToken);

    }
}

