using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Shared.Core;
using PersonalPortfolio.Shared.Storage.Abstractions;

namespace PersonalPortfolio.Shared.Storage.Queries
{
    public class SecurityQueryService : ISecurityQueryService
    {
        private readonly IContextFactory<PortfolioDbContext> _ctxFactory;
        private readonly IMapper<Security, SecurityInfo> _mapper;

        public SecurityQueryService(IContextFactory<PortfolioDbContext> ctxFactory, IMapper<Security, SecurityInfo> mapper)
        {
            _ctxFactory = ctxFactory;
            _mapper = mapper;
        }

        public async Task<SecurityInfo> GetSecurityInfoByCode(string code, CancellationToken token)
        {
            await using var ctx = _ctxFactory.CreateDbContext();

            return await ctx.Securities
                .Where(s => s.Ticker == code)
                .Include(s => s.Type)
                .Include(s => s.BaseCurrency)
                .Select(_mapper.GetProjection())
                .FirstOrDefaultAsync(token)
                .ConfigureAwait(false);
        }

        public async Task<List<SecurityInfo>> GetAllSecurityInfos(CancellationToken token)
        {
            await using var ctx = _ctxFactory.CreateDbContext();

            return await ctx.Securities
                .Include(s => s.Type)
                .Include(s => s.BaseCurrency)
                .Select(_mapper.GetProjection())
                .ToListAsync(token)
                .ConfigureAwait(false);
        }
    }
}
