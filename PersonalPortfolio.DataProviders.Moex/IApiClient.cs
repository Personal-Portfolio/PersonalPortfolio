using System.Threading;
using System.Threading.Tasks;

namespace PersonalPortfolio.DataProviders.Moex
{
    internal interface IApiClient
    {
        Task<string> FetchAsync(string url, CancellationToken token);
    }
}