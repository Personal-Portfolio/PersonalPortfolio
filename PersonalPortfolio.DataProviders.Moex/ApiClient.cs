using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PersonalPortfolio.DataProviders.Moex
{
    internal class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IApiClient> _logger;

        public ApiClient(HttpClient httpClient, ILogger<IApiClient> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> FetchAsync(string url, CancellationToken token)
        {
            using var response = await _httpClient.GetAsync(url, token);
            using var responseMessage = response.EnsureSuccessStatusCode();

            var json = await responseMessage.Content.ReadAsStringAsync();

            _logger.LogDebug("Received data: {0}", json);

            return json;
        }
    }
}
