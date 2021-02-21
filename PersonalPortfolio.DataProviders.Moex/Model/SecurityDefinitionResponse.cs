using Newtonsoft.Json;

namespace PersonalPortfolio.DataProviders.Moex.Model
{
    internal sealed class SecurityDefinitionResponse
    {
        [JsonProperty("boards")]
        public SecurityBoard[] Boards { get; set; }
    }
}
