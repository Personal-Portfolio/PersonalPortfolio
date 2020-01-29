using Newtonsoft.Json;

namespace PersonalPortfolio.DataProviders.Moex.Model
{
    internal sealed class Market
    {
        [JsonProperty("NAME")]
        public string Name { get; set; }
    }
}
