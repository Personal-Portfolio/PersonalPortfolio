using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalPortfolio.Shared.Core
{
    public interface ISecurityInfoService
    {
        Task<IEnumerable<ISecurityPrice>> GetHistory(string securityCode, CancellationToken token);
    }
}
