using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PersonalPortfolio.Shared.Core;

namespace PersonalPortfolio.Shared.Storage.Abstractions
{
    public interface ICurrencyCommandService
    {
        Task<int> AddOrUpdateAsync(IEnumerable<CurrencyInfo> infos, CancellationToken token);
        Task<int> AddRatesAsync(IEnumerable<(DateTime, string, string, decimal, string)> rates, CancellationToken token);
    }
}