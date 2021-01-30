using System.Linq;
using Fluxor;
using PersonalPortfolio.TaxCalc.Client.Store.BrokerReportImport.Actions;
using PersonalPortfolio.TaxCalc.Client.Store.BrokerReportImport.States;

namespace PersonalPortfolio.TaxCalc.Client.Store.BrokerReportImport.Reducers
{
    internal class SetTaxationTradeReportActionReducer : Reducer<TaxationTradeReportItemState, SetTaxationTradeReportAction>
    {
        public override TaxationTradeReportItemState Reduce(TaxationTradeReportItemState state, SetTaxationTradeReportAction action) =>
            new(action.Reports.ToList());
    }
}