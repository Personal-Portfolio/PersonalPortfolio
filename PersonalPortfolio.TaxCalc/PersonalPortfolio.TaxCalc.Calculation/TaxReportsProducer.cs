using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using PersonalPortfolio.Shared.Abstractions;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.Calculation
{
    internal class TaxReportsProducer : ITaxReportsProducer
    {
        private readonly IRatesProvider _ratesProvider;

        public TaxReportsProducer(IRatesProvider ratesProvider)
        {
            _ratesProvider = ratesProvider;
        }

        public async IAsyncEnumerable<TaxationTradeReportItem> ProduceReportsByTradesAsync(
            string symbol,
            IEnumerable<Asset> assets,
            Currency currency,
            int quantity,
            decimal tradeIncome,
            decimal tradeFee,
            DateTime tradeDate,
            decimal taxRate,
            [EnumeratorCancellation] CancellationToken cancellation)
        {
            var currencyCode = currency.ToString("G");

            var sellConversionRate = await GetRateByDateAndCode(tradeDate, currencyCode, cancellation)
                .ConfigureAwait(false);

            var sellPrice = tradeIncome / quantity;
            var sellFee = tradeFee / quantity;
            var sellTaxationPrice = sellPrice * sellConversionRate;
            var sellTaxationFee = sellFee * sellConversionRate;

            foreach (var asset in assets)
            {
                if(cancellation.IsCancellationRequested)
                    yield break;

                var conversionRate = await GetRateByDateAndCode(asset.SettleDate, currencyCode, cancellation)
                    .ConfigureAwait(false);

                var report = new TaxationTradeReportItem
                {
                    AssetSymbol = symbol,
                    AssetOutputDate = tradeDate,
                    AssetInputDate = asset.SettleDate,
                    Currency = currency,
                    BuyPrice = asset.Price,
                    BuyFee = asset.Fee,
                    TaxationBuyPrice = asset.Price * conversionRate,
                    TaxationBuyFee = asset.Fee * conversionRate,
                    SellPrice = sellPrice,
                    SellFee = sellFee,
                    TaxationSellPrice = sellTaxationPrice,
                    TaxationSellFee = sellTaxationFee,
                    Result = sellPrice - asset.Price - asset.Fee - sellFee,
                    TaxationResult = (sellPrice - sellFee) * sellConversionRate -
                                     (asset.Price + asset.Fee) * conversionRate
                };

                report.CalculatedTax = report.TaxationResult * taxRate;

                yield return report;
            }
        }

        private async Task<decimal> GetRateByDateAndCode(DateTime dateTime, string currencyCode, CancellationToken cancellation)
        {
            var buyRates = await _ratesProvider.GetRatesForDateAsync(dateTime, cancellation)
                .ConfigureAwait(false);

            var buyRate = buyRates.FirstOrDefault(r =>
                r.Code.Equals(currencyCode, StringComparison.OrdinalIgnoreCase));

            if (buyRate == null)
                throw new InvalidOperationException($"Unknown rate on date ({dateTime}).");

            return buyRate.Value / buyRate.Size;
        }
    }
}