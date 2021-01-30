using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using PersonalPortfolio.Shared.Abstractions;

namespace PersonalPortfolio.DataProviders.Cbr
{
    internal class RatesProvider : IRatesProvider
    {
        private readonly ICbrClient _client;

        public RatesProvider(ICbrClient client)
        {
            _client = client ?? throw new ArgumentNullException();
        }

        public async Task<IEnumerable<IRate>> GetRatesForDateAsync(DateTime dateTime, CancellationToken cancellation)
        {
            cancellation.ThrowIfCancellationRequested();

            var usdRateOnSellDate = await _client.GetCursOnDateXMLAsync(dateTime)
                .ConfigureAwait(false);

            var dcs = new DataContractSerializer(typeof(DailyRates));
            using var reader = new XmlNodeReader(usdRateOnSellDate);

            if (!(dcs.ReadObject(reader) is DailyRates result))
                throw new InvalidOperationException("Failed to get daily rates.");

            return result;
        }
    }
}