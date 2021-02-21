using Newtonsoft.Json;

namespace PersonalPortfolio.DataProviders.Moex.Model
{
    internal sealed class SecurityHistoryResponse
    {
        [JsonProperty("history")]
        public SecurityHistoryData[] History { get; set; }

        [JsonProperty("history.cursor")]
        public Cursor[] Cursor { get; set; }
    }
}
