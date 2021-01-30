using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalPortfolio.Shared.Abstractions
{
    public interface IRatesProvider
    {
        Task<IEnumerable<IRate>> GetRatesForDateAsync(DateTime dateTime, CancellationToken cancellation);
    }
}