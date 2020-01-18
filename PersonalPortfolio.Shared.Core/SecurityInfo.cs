using PersonalPortfolio.Shared.Abstractions;

namespace PersonalPortfolio.Shared.Core
{
    public class SecurityInfo: ISymbol
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public CurrencyInfo BaseCurrency { get; set; }
    }
}
