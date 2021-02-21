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
        private readonly PortfolioDbContext _ctx;
        private readonly IMapper<Security, SecurityInfo> _mapper;

        public SecurityQueryService(PortfolioDbContext ctx, IMapper<Security, SecurityInfo> mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<SecurityInfo> GetSecurityInfoByCodeAsync(string code, CancellationToken token)
        {
            return await _ctx.Securities
                .Where(s => s.Ticker == code)
                .Include(s => s.Type)
                .Include(s => s.BaseCurrency)
                .Select(_mapper.GetProjection())
                .FirstOrDefaultAsync(token)
                .ConfigureAwait(false);
        }

        public async Task<List<SecurityInfo>> GetAllSecurityInfosAsync(CancellationToken token)
        {
            return await _ctx.Securities
                .Include(s => s.Type)
                .Include(s => s.BaseCurrency)
                .Select(_mapper.GetProjection())
                .ToListAsync(token)
                .ConfigureAwait(false);
        }
    }
}
