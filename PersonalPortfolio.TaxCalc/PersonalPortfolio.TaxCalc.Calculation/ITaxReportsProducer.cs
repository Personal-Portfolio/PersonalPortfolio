using System;
using System.Collections.Generic;
using System.Threading;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.Calculation
{
    internal interface ITaxReportsProducer
    {
        IAsyncEnumerable<TaxationTradeReportItem> ProduceReportsByTradesAsync(
            string symbol,
            IEnumerable<Asset> assets,
            Currency currency,
            int quantity,
            decimal tradeIncome,
            decimal tradeFee,
            DateTime tradeDate,
            decimal taxRate,
            CancellationToken cancellation);
    }
}