using Newtonsoft.Json;

namespace PersonalPortfolio.DataProviders.Moex.Model
{
    internal sealed class SecurityListResponse
    {
        [JsonProperty("securities")]
        public SecurityInfo[] Securities { get; set; }

        [JsonProperty("marketdata")]
        public SecurityMarketData[] MarketData { get; set; }
    }
}
