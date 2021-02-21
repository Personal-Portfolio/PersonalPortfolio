using Newtonsoft.Json;

namespace PersonalPortfolio.DataProviders.Moex.Model
{
    internal sealed class MarketsResponse
    {
        [JsonProperty("markets")]
        public Market[] Markets { get; set; }
    }
}
