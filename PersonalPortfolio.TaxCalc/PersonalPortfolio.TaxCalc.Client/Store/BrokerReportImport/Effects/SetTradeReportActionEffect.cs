using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fluxor;
using Microsoft.Extensions.Logging;
using PersonalPortfolio.TaxCalc.Client.Store.BrokerReportImport.Actions;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.Client.Store.BrokerReportImport.Effects
{
    internal class SetTradeReportActionEffect : Effect<SetTradeReportAction>
    {
        private readonly ITaxReportGenerator _taxReportGenerator;
        private readonly ILogger<SetTradeReportActionEffect> _logger;

        public SetTradeReportActionEffect(ITaxReportGenerator parser, ILogger<SetTradeReportActionEffect> logger)
        {
            _taxReportGenerator = parser ?? throw new ArgumentNullException(nameof(parser));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task HandleAsync(SetTradeReportAction trigger, IDispatcher dispatcher)
        {
            var list = new List<TaxationTradeReportItem>();

            // TODO: Add cancellation
            var reports = _taxReportGenerator.GenerateReportsByTrades(trigger.TradeItems, CancellationToken.None);

            await foreach (var item in reports.WithCancellation(CancellationToken.None).ConfigureAwait(false))
            {
                list.Add(item);
            }

            _logger.LogInformation($"Found {list.Count} taxation trades.");

            var action = new SetTaxationTradeReportAction(list);
            dispatcher.Dispatch(action);
        }
    }
}
