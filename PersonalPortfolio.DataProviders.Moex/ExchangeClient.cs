using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PersonalPortfolio.DataProviders.Moex.Model;

namespace PersonalPortfolio.DataProviders.Moex
{
    internal sealed class ExchangeClient : IExchangeClient
    {
        private const string IssJsonSettings = "iss.meta=off&iss.json=extended";
        private readonly IApiClient _apiClient;

        public ExchangeClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        private static T GetResponseFromJson<T>(string json)
        {
            var jToken = JToken.Parse(json);

            if (!jToken.HasValues || jToken.Type != JTokenType.Array)
                return default;

            var jArray = (JArray)jToken;
            var valueToken = jArray[1];
            return valueToken.ToObject<T>();
        }

        public async Task<EnginesResponse> GetEnginesAsync(CancellationToken token)
        {
            var json = await _apiClient.FetchAsync($"engines.json?engines.columns=name&{IssJsonSettings}", token);

            return GetResponseFromJson<EnginesResponse>(json);
        }

        public async Task<SecurityListResponse> GetMarketSecurities(string engine, string market, CancellationToken token)
        {
            var json = await  _apiClient.FetchAsync($"engines/{engine}/markets/{market}/securities.json?securities.columns=SECID,NAME,CURRENCYID&iss.only=securities&{IssJsonSettings}", token);

            return GetResponseFromJson<SecurityListResponse>(json);
        }

        public async Task<MarketsResponse> GetMarketsByEngine(string engine, CancellationToken token)
        {
            var json = await  _apiClient.FetchAsync($"engines/{engine}/markets.json?markets.columns=NAME&{IssJsonSettings}", token);

            return GetResponseFromJson<MarketsResponse>(json);
        }

        public async Task<SecurityDefinitionResponse> GetSecurityDefinition(string securityCode, CancellationToken token)
        {
            var json = await  _apiClient.FetchAsync(
                $"securities/{securityCode}.json?" +
                "boards.columns=secid,boardid,engine,market,history_from,history_till,currencyid,is_primary&" +
                IssJsonSettings,
                token);

            return GetResponseFromJson<SecurityDefinitionResponse>(json);
        }

        public async Task<SecurityListResponse> GetSecurityDetails(string engine, string market, string securityCode,
            CancellationToken token)
        {
            var json = await  _apiClient.FetchAsync(
                $"engines/{engine}/markets/{market}/securities/{securityCode}.json?" +
                "marketdata.columns=SECID,BOARDID,SYSTIME,TRADEDATE,LASTVALUE,OPENVALUE,HIGH,LOW&" +
                "securities.columns=SECID,NAME,CURRENCYID&" +
                "iss.only=securities,marketdata&"+
                IssJsonSettings,
                token);

            return GetResponseFromJson<SecurityListResponse>(json);
        }

        public async Task<SecurityHistoryResponse> GetSecurityHistory(
            string engine,
            string market,
            string securityCode,
            DateTime from,
            DateTime till,
            int index,
            CancellationToken token)
        {
            var json = await  _apiClient.FetchAsync(
                $"history/engines/{engine}/markets/{market}/securities/{securityCode}.json?" +
                "history.columns=BOARDID,TRADEDATE,SECID,OPEN,LOW,HIGH,LEGALCLOSEPRICE,WAPRICE&" +
                $"from={from:yyyy-MM-dd}&till={till:yyyy-MM-dd}&start={index}&" +
                IssJsonSettings,
                token);

            return GetResponseFromJson<SecurityHistoryResponse>(json);
        }
    }
}