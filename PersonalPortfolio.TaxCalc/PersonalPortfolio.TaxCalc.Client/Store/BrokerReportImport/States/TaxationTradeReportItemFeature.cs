using System.Collections.Generic;
using Fluxor;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.Client.Store.BrokerReportImport.States
{
    internal class TaxationTradeReportItemFeature : Feature<TaxationTradeReportItemState>
    {
        public override string GetName() => nameof(TaxationTradeReportItemState);

        protected override TaxationTradeReportItemState GetInitialState() =>
            new(new List<TaxationTradeReportItem>());
    }
}