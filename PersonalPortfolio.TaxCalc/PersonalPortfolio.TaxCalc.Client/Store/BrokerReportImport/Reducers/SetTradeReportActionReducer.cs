using Fluxor;
using PersonalPortfolio.TaxCalc.Client.Store.BrokerReportImport.Actions;
using PersonalPortfolio.TaxCalc.Client.Store.BrokerReportImport.States;

namespace PersonalPortfolio.TaxCalc.Client.Store.BrokerReportImport.Reducers
{
    internal class SetTradeReportActionReducer : Reducer<TradeItemState, SetTradeReportAction>
    {
        public override TradeItemState Reduce(TradeItemState state, SetTradeReportAction action) =>
            new(action.TradeItems);
    }
}