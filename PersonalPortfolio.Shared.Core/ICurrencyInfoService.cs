using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalPortfolio.Shared.Core
{
    public interface ICurrencyInfoService
    {
        Task<IEnumerable<(DateTime, string, string, decimal, string)>> GetHistoricalRatesForCurrencyList(IReadOnlyList<string> filter, CancellationToken token);
    }
}