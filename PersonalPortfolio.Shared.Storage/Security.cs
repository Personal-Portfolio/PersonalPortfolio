using System.Collections.Generic;

namespace PersonalPortfolio.Shared.Storage
{
    public class Security: Entity
    {
        public string Ticker { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public SecurityType Type { get; set; }
        public Currency BaseCurrency { get; set; }
        public int BaseCurrencyId { get; set; }
        public ICollection<SecurityPrice> Prices { get; set; }
    }
}
