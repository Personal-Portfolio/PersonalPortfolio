using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cursively;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.BrokerParsing
{
    internal class BrokerReportParser : IBrokerReportParser
    {
        private readonly Decoder _decoder;
        private readonly ITradeItemFactory _factory;

        public BrokerReportParser(Decoder decoder, ITradeItemFactory factory)
        {
            _decoder = decoder ?? throw new ArgumentNullException(nameof(decoder));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<List<TradeItem>> ParseReportAsync(Stream fileStream, CancellationToken cancellation)
        {
            var result = new List<TradeItem>();

            var myVisitor = new BrokerReportVisitor(_decoder, 1000, record => 
            {
                var item = _factory.Create(record);
                if (item == null)
                    return;
                result.Add(item);
            });

            await CsvAsyncInput
                .ForStream(fileStream)
                .ProcessAsync(myVisitor, cancellationToken: cancellation);

            return result;
        }
    }
}