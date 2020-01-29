using System;
using Newtonsoft.Json;

namespace PersonalPortfolio.DataProviders.Moex.Model
{
    internal sealed class SecurityBoard
    {
        [JsonProperty("secid")]
        public string Code { get; set; }

        [JsonProperty("boardid")]
        public string BoardId { get; set; }

        [JsonProperty("engine")]
        public string EngineName { get; set; }

        [JsonProperty("market")]
        public string MarketName { get; set; }

        [JsonProperty("history_from")]
        public DateTime? HistoryFrom { get; set; }

        [JsonProperty("history_till")]
        public DateTime? HistoryTill { get; set; }

        [JsonProperty("currencyid")]
        public string CurrencyId { get; set; }

        [JsonProperty("is_primary")]
        public bool IsPrimary { get; set; }
    }
}
