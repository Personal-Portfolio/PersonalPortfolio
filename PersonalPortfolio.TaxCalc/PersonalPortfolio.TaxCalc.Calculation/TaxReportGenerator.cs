using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.Calculation
{
    internal class TaxReportGenerator : ITaxReportGenerator
    {
        private readonly ITaxReportsProducer _taxReportsProducer;

        private const decimal TaxRate = 0.13m; // TODO: options;

        public TaxReportGenerator(ITaxReportsProducer taxReportsProducer)
        {
            _taxReportsProducer = taxReportsProducer;
        }

        public async IAsyncEnumerable<TaxationTradeReportItem> GenerateReportsByTrades(
            List<TradeItem> report,
            [EnumeratorCancellation] CancellationToken cancellation)
        {
            var inventories = report
                .Where(r => !string.IsNullOrEmpty(r.Symbol) && r.Type == TradeType.Buy)
                .OrderBy(r => r.TradeDate)
                .GroupBy(r => r.Symbol)
                .ToDictionary(g => g.Key, SetInventory);

            var sellTrades = report
                .OrderBy(r => r.TradeDate)
                .Where(r => !string.IsNullOrEmpty(r.Symbol) && r.Type == TradeType.Sell);

            foreach (var tradeItem in sellTrades)
            {
                if (!inventories.TryGetValue(tradeItem.Symbol, out var inventory)
                    || inventory == null)
                    throw new InvalidOperationException(
                        $"No inventory was found. There should be at least one buy entry in report for symbol {tradeItem.Symbol} to sell.");

                var records = HandSellRecord(inventory, tradeItem, cancellation);

                await foreach (var taxReport in records.WithCancellation(cancellation).ConfigureAwait(false))
                    yield return taxReport;
            }
        }

        private async IAsyncEnumerable<TaxationTradeReportItem> HandSellRecord(
            Inventory inventory,
            TradeItem tradeItem,
            [EnumeratorCancellation] CancellationToken cancellation)
        {
            var soldAssets = inventory.SellAssets(tradeItem.Amount);

            var reports =
                _taxReportsProducer.ProduceReportsByTradesAsync(tradeItem.Symbol, soldAssets,
                    tradeItem.Currency, tradeItem.Amount, tradeItem.Cost,
                    tradeItem.Fee, tradeItem.TradeDate, TaxRate, cancellation)
                .ConfigureAwait(false);

            await foreach (var taxReport in reports.WithCancellation(cancellation).ConfigureAwait(false))
                yield return taxReport;
        }

        private static Inventory SetInventory(IEnumerable<TradeItem> buyTrades)
        {
            var inventory = new Inventory();
            foreach (var tradeItem in buyTrades)
            {
                inventory.InputAssets(tradeItem.Amount, tradeItem.TradeDate, tradeItem.Cost, tradeItem.Fee);
            }

            return inventory;
        }
    }
}