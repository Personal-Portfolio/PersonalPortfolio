using System.Collections.Generic;

namespace PersonalPortfolio.Shared.Storage
{
    public class Currency: Entity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public ICollection<CurrencyRate> Rates { get; set; }
    }
}
