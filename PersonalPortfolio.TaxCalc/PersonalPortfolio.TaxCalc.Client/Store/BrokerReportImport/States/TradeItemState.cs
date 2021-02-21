using System.Collections.Generic;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.Client.Store.BrokerReportImport.States
{
    public class TradeItemState
    {
        public TradeItemState(List<TradeItem> inputItems)
        {
            InputItems = inputItems;
        }

        public List<TradeItem> InputItems { get; }
    }
}
