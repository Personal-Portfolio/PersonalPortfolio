using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PersonalPortfolio.Shared.Core;

namespace PersonalPortfolio.Shared.Storage.Abstractions
{
    public interface ICurrencyQueryService
    {
        Task<CurrencyInfo> GetCurrencyInfoByCodeAsync(string code, CancellationToken token);
        Task<List<CurrencyInfo>> GetRegisteredCurrenciesAsync(CancellationToken token);
    }
}