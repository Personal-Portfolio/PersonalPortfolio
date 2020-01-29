using System;
using System.Threading;
using System.Threading.Tasks;
using PersonalPortfolio.DataProviders.Moex.Model;

namespace PersonalPortfolio.DataProviders.Moex
{
    internal interface IExchangeClient
    {
        Task<EnginesResponse> GetEnginesAsync(CancellationToken token);
        Task<SecurityListResponse> GetMarketSecurities(string engine, string market, CancellationToken token);
        Task<MarketsResponse> GetMarketsByEngine(string engine, CancellationToken token);
        Task<SecurityDefinitionResponse> GetSecurityDefinition(string securityCode, CancellationToken token);

        Task<SecurityListResponse> GetSecurityDetails(string engine, string market, string securityCode,
            CancellationToken token);

        Task<SecurityHistoryResponse> GetSecurityHistory(string engine, string market, string securityCode,
            DateTime from,
            DateTime till, int index, CancellationToken token);
    }
}
