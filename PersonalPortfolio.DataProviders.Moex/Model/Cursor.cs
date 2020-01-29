using Newtonsoft.Json;

namespace PersonalPortfolio.DataProviders.Moex.Model
{
    internal class Cursor
    {
        [JsonProperty("INDEX")]
        public int Index { get; set; }

        [JsonProperty("TOTAL")]
        public int Total { get; set; }

        [JsonProperty("PAGESIZE")]
        public int PageSize { get; set; }
    }
}