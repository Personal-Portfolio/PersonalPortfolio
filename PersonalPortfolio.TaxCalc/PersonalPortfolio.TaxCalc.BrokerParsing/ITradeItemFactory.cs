using System.Collections.Generic;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.BrokerParsing
{
    internal interface ITradeItemFactory
    {
        TradeItem Create(IDictionary<string, string> tradeRecord);
    }
}