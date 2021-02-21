using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PersonalPortfolio.Shared.Core;

namespace PersonalPortfolio.DataProviders.EuroFx
{
    public class CurrencyInfoService : ICurrencyInfoService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CurrencyInfoService> _logger;

        public CurrencyInfoService(HttpClient httpClient, ILogger<CurrencyInfoService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<IEnumerable<(DateTime, string, string, decimal, string)>> GetHistoricalRatesForCurrencyList(IReadOnlyList<string> filter, CancellationToken token)
        {
            return UpdateCurrencies(filter, token);
        }

        private async Task<IEnumerable<(DateTime, string, string, decimal, string)>> UpdateCurrencies(IReadOnlyList<string> filter, CancellationToken token)
        {
            var rawCurrencyList = await GetXmlFromSource(token)
                .ConfigureAwait(false);
            return await ParseHttpResponse(filter, rawCurrencyList, token)
                .ConfigureAwait(false);
        }

        private async Task<HttpResponseMessage> GetXmlFromSource(CancellationToken token)
        {
            var response = await _httpClient
                .GetAsync(new Uri("stats/eurofxref/eurofxref-hist.xml", UriKind.Relative), token)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException("Failed to update currencies.");

            return response;
        }

        private static async Task<IEnumerable<(DateTime, string, string, decimal, string)>> ParseHttpResponse(
            IReadOnlyList<string> filter,
            HttpResponseMessage rawCurrencyList,
            CancellationToken token)
        {
            var xmlStream = await rawCurrencyList.Content.ReadAsStreamAsync()
                .ConfigureAwait(false);

            var parser = new CurrencyParser("EUR", filter);

            return await parser.ParseResponse(xmlStream, token)
                .ConfigureAwait(false);
        }
    }

}
