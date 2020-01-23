using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalPortfolio.Shared.Storage.Abstractions
{
    public interface IBulkCommandsService
    {
        Task<int> InsertAsync<T>(IList<T> entities, CancellationToken token) where T: class;
    }
}
