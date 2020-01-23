using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Shared.Core;
using PersonalPortfolio.Shared.Storage.Abstractions;

namespace PersonalPortfolio.Shared.Storage.Queries
{
    public class CurrencyQueryService : ICurrencyQueryService
    {
        private readonly PortfolioDbContext _ctx;
        private readonly IMapper<Currency, CurrencyInfo> _mapper;

        public CurrencyQueryService(PortfolioDbContext ctx, IMapper<Currency, CurrencyInfo> mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<CurrencyInfo> GetCurrencyInfoByCodeAsync(string code, CancellationToken token)
        {
            return await _ctx.Currencies
                .Where(s => s.Code == code)
                .Select(_mapper.GetProjection())
                .FirstOrDefaultAsync(token)
                .ConfigureAwait(false);
        }

        public async Task<List<CurrencyInfo>> GetRegisteredCurrenciesAsync(CancellationToken token)
        {
            return await _ctx.Currencies
                .Select(_mapper.GetProjection())
                .ToListAsync(token)
                .ConfigureAwait(false);
        }
    }
}
