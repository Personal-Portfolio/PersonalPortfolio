using System;
using Newtonsoft.Json;

namespace PersonalPortfolio.DataProviders.Moex.Model
{
    internal sealed class SecurityMarketData
    {
        [JsonProperty("SECID")]
        public string Code { get; set; }

        [JsonProperty("BOARDID")]
        public string BoardId { get; set; }

        [JsonProperty("SYSTIME")]
        public DateTime SysTime { get; set; }

        [JsonProperty("TRADEDATE")]
        public DateTime TradeDate { get; set; }

        [JsonProperty("LASTVALUE")]
        public decimal? Last { get; set; }

        [JsonProperty("OPENVALUE")]
        public decimal? Open { get; set; }

        [JsonProperty("HIGH")]
        public decimal? High { get; set; }

        [JsonProperty("LOW")]
        public decimal? Low { get; set; }
    }
}
