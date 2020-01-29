using Newtonsoft.Json;

namespace PersonalPortfolio.DataProviders.Moex.Model
{
    internal sealed class Engine
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
