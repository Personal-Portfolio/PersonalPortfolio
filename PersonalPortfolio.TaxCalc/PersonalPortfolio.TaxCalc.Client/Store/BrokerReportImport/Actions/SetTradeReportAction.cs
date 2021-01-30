using System.Collections.Generic;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.Client.Store.BrokerReportImport.Actions
{
    internal class SetTradeReportAction
    {
        public List<TradeItem> TradeItems { get; }

        public SetTradeReportAction(List<TradeItem> tradeItems)
        {
            TradeItems = tradeItems;
        }
    }
}
