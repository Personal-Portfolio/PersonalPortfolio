using System.Collections.Generic;
using System.Threading;

namespace PersonalPortfolio.TaxCalc.Core
{
    /// <summary>
    /// Provide methods to create tax reports
    /// </summary>
    public interface ITaxReportGenerator
    {
        IAsyncEnumerable<TaxationTradeReportItem> GenerateReportsByTrades(List<TradeItem> report, CancellationToken cancellation);
    }
}