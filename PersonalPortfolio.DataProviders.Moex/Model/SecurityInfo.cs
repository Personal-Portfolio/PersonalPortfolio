using Newtonsoft.Json;

namespace PersonalPortfolio.DataProviders.Moex.Model
{
    internal sealed class SecurityInfo
    {
        [JsonProperty("SECID")]
        public string Code { get; set; }

        [JsonProperty("NAME")]
        public string Name { get; set; }

        [JsonProperty("CURRENCYID")]
        public string CurrencyId { get; set; }
    }
}
