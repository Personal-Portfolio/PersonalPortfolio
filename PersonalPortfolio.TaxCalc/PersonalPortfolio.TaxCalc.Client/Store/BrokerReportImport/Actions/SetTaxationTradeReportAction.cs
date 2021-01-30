using System.Collections.Generic;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.Client.Store.BrokerReportImport.Actions
{
    internal class SetTaxationTradeReportAction
    {
        public IEnumerable<TaxationTradeReportItem> Reports { get; }

        public SetTaxationTradeReportAction(IEnumerable<TaxationTradeReportItem> reports)
        {
            Reports = reports;
        }
    }
}