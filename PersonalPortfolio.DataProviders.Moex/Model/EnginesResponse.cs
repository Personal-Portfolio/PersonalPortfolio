using Newtonsoft.Json;

namespace PersonalPortfolio.DataProviders.Moex.Model
{
    internal sealed class EnginesResponse
    {
        [JsonProperty("engines")]
        public Engine[] Engines { get; set; }
    }
}
