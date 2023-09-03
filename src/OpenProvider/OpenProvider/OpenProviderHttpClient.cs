using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace OpenProvider
{
    public class OpenProviderHttpClient : HttpClient, IOpenProviderHttpClient
    {
        private static string? _bearerToken;
        private readonly OpenProviderConfiguration _config;
        private readonly HttpClient _httpClient;

        private const string authUrl = "https://api.openprovider.eu/v1beta/auth/login";

        public OpenProviderHttpClient(OpenProviderConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public new async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption responseHeadersRead,
           CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessTokenAsync());
            return await base.SendAsync(request, responseHeadersRead, cancellationToken);
        }

        private async Task<string> GetAccessTokenAsync()
        {
            if (_bearerToken != null && String.IsNullOrWhiteSpace(_bearerToken))
            {
                return _bearerToken;
            }

            var body = $"{{\"username\": \"{_config.Username}\", \"password\": \"{_config.Password}\"}}";
            var content = new StringContent(body, mediaType: MediaTypeHeaderValue.Parse("application/json"));

            //call
            var res = await _httpClient.PostAsync(authUrl, content);
            var apiRes = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
            {
                throw new ApiException("Unable to get access token", 401,
                    apiRes, null,null);
            }
            
            var jo = JObject.Parse(apiRes);
            _bearerToken = jo["data"]!["token"]!.ToString();
            return _bearerToken;
        }
    }
}

