using System;
using PersonalPortfolio.Shared.Abstractions;

namespace PersonalPortfolio.Shared.Core
{
    public interface IRatesManager
    {
        bool RegisterCurrency(ISymbol targetCurrency);

        bool CanConvert(ISymbol sourceSymbol, ISymbol targetCurrency);

        float GetRate(ISymbol sourceSymbol, ISymbol targetCurrency, DateTime date);
    }
}
