using System.Collections.Generic;
using Fluxor;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.Client.Store.BrokerReportImport.States
{
    internal class TradeItemFeature : Feature<TradeItemState>
    {
        public override string GetName() => nameof(TradeItemState);

        protected override TradeItemState GetInitialState() =>
            new(new List<TradeItem>());
    }
}