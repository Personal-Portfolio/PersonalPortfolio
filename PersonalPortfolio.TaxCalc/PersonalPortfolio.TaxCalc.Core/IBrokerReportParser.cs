using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalPortfolio.TaxCalc.Core
{
    /// <summary>
    /// Provide methods to get entries representing data from broker reports
    /// </summary>
    public interface IBrokerReportParser
    {
        Task<List<TradeItem>> ParseReportAsync(Stream fileStream, CancellationToken cancellation);
    }
}
