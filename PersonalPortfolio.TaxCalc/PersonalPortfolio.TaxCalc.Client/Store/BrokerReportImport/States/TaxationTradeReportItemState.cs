using System.Collections.Generic;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.Client.Store.BrokerReportImport.States
{
    internal class TaxationTradeReportItemState
    {
        public TaxationTradeReportItemState(List<TaxationTradeReportItem> reportItems)
        {
            ReportItems = reportItems;
        }

        public List<TaxationTradeReportItem> ReportItems { get; }
    }
}