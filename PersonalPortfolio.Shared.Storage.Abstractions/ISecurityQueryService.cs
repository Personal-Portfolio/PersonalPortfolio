using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PersonalPortfolio.Shared.Core;

namespace PersonalPortfolio.Shared.Storage.Queries
{
    public interface ISecurityQueryService
    {
        Task<SecurityInfo> GetSecurityInfoByCode(string code, CancellationToken token);
        Task<List<SecurityInfo>> GetAllSecurityInfos(CancellationToken token);
    }
}