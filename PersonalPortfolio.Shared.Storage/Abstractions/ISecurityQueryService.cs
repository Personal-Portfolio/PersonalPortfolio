using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PersonalPortfolio.Shared.Core;

namespace PersonalPortfolio.Shared.Storage.Abstractions
{
    public interface ISecurityQueryService
    {
        Task<SecurityInfo> GetSecurityInfoByCodeAsync(string code, CancellationToken token);
        Task<List<SecurityInfo>> GetAllSecurityInfosAsync(CancellationToken token);
    }
}