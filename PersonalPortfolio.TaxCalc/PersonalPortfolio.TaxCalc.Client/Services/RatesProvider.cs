using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using PersonalPortfolio.Shared.Abstractions;
using PersonalPortfolio.TaxCalc.Client.Models;

namespace PersonalPortfolio.TaxCalc.Client.Services
{
    internal class RatesProvider : IRatesProvider
    {
        private readonly HttpClient _client;

        public RatesProvider(HttpClient client)
        {
            _client = client ??  throw new ArgumentNullException();
        }

        public async Task<IEnumerable<IRate>> GetRatesForDateAsync(DateTime dateTime, CancellationToken cancellation) =>
            await _client.GetFromJsonAsync<List<Rate>>($"Rates/{dateTime.ToBinary()}", cancellation)
                .ConfigureAwait(false);
    }
}