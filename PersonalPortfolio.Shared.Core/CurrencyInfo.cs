using PersonalPortfolio.Shared.Abstractions;

namespace PersonalPortfolio.Shared.Core
{
    public class CurrencyInfo: ISymbol
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
