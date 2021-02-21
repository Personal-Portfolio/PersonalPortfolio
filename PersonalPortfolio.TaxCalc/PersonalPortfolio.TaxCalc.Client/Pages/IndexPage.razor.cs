using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fluxor;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using PersonalPortfolio.TaxCalc.Client.Store.BrokerReportImport.Actions;
using PersonalPortfolio.TaxCalc.Client.Store.BrokerReportImport.States;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.Client.Pages
{
    public sealed partial class IndexPage
    {
        private readonly IBrokerReportParser _brokerReportParser;
        private readonly IState<TradeItemState> _tradeItemState;
        private readonly ILogger<IndexPage> _logger;
        private readonly IDispatcher _dispatcher;

        public IndexPage(
            IBrokerReportParser brokerReportParser,
            IState<TradeItemState> tradeItemState,
            ILogger<IndexPage> logger,
            IDispatcher dispatcher)
        {
            _brokerReportParser = brokerReportParser;
            _tradeItemState = tradeItemState;
            _logger = logger;
            _dispatcher = dispatcher;
        }

        private async Task OnFileSelection(IEnumerable<IBrowserFile> files)
        {
            _logger.LogInformation("Started reading CSV file.");

            var reports = new List<TradeItem>();

            foreach (var file in files)
            {
                await using var fileStream = file.OpenReadStream();

                var report = await _brokerReportParser
                    .ParseReportAsync(fileStream, CancellationToken.None)
                    .ConfigureAwait(false);

                reports.AddRange(report);
            }

            _logger.LogInformation("Finished reading CSV file(s).");
            _logger.LogInformation($"Created {reports.Count} records.");

            var action = new SetTradeReportAction(reports);
            _dispatcher.Dispatch(action);
        }

        private void ResetTradeReports()
        {
            var action = new SetTradeReportAction(new List<TradeItem>());
            _dispatcher.Dispatch(action);
        }
    }
}
