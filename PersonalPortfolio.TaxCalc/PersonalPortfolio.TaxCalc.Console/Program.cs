using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PersonalPortfolio.DataProviders.Cbr;
using PersonalPortfolio.TaxCalc.BrokerParsing;
using PersonalPortfolio.TaxCalc.Calculation;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.Console
{
    internal class Program
    {
        public static async Task Main(params string[] args)
        {
            var reports = new List<TradeItem>();

            var services = 
                new ServiceCollection()
                    .AddCbrDataProviders("http://www.cbr.ru/DailyInfoWebServ/DailyInfo.asmx")
                    .AddInteractiveBrokersStockReportParser()
                    .AddTaxReportGenerator()
                    .BuildServiceProvider(true);

            var parser = services.GetRequiredService<IBrokerReportParser>();

            foreach (var s in args)
            {
                await using var file = File.OpenRead(s);

                System.Console.WriteLine("Started reading CSV file.");

                var report = await parser.ParseReportAsync(file, CancellationToken.None);

                reports.AddRange(report);

                System.Console.WriteLine("Finished reading CSV file.");
            }

            var taxCulture = Currency.Rub.ToCulture();

            var reportGenerator = services.GetRequiredService<ITaxReportGenerator>();

            var taxReports = reportGenerator.GenerateReportsByTrades(reports, CancellationToken.None);

            await PrintTaxReports(taxReports, taxCulture);

            //var taxToPay = await GetTax(taxReports).ConfigureAwait(false);

            //Console.WriteLine($"Tax to pay: {taxToPay.ToString("C3", taxCulture)}.");

            System.Console.ReadKey();
        }

        private static async ValueTask<decimal> GetTax(IAsyncEnumerable<TaxationTradeReportItem> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var result = 0m;

            await foreach (var item in source.ConfigureAwait(false))
            {
                result += item.TaxationResult;
            }

            return result * 0.13m;
        }

        private static async Task PrintTaxReports(IAsyncEnumerable<TaxationTradeReportItem> reports, IFormatProvider taxCulture)
        {
            System.Console.WriteLine("Symbol|Date bought|Buy Price|Buy Price(tax)|Buy Fee|Buy Fee(tax)|Sell Price|Sell Price(tax)|Sell Fee|Sell Fee(tax)|Result|Result(tax)|Tax to pay");

            await foreach (var taxationTradeReportItem in reports)
            {
                var culture = taxationTradeReportItem.Currency.ToCulture();
                var sb = new StringBuilder();

                sb.Append(taxationTradeReportItem.AssetSymbol)
                    .Append("\t|")
                    .AppendFormat(culture, "{0:d}", taxationTradeReportItem.AssetInputDate)
                    .Append("\t|")
                    .AppendFormat(culture, "{0:C3}", taxationTradeReportItem.BuyPrice)
                    .Append("\t|")
                    .AppendFormat(taxCulture, "{0:C3}", taxationTradeReportItem.TaxationBuyPrice)
                    .Append("\t|")
                    .AppendFormat(culture, "{0:C3}", taxationTradeReportItem.BuyFee)
                    .Append("\t|")
                    .AppendFormat(taxCulture, "{0:C3}", taxationTradeReportItem.TaxationBuyFee)
                    .Append("\t|")
                    .AppendFormat(culture, "{0:C3}", taxationTradeReportItem.SellPrice)
                    .Append("\t|")
                    .AppendFormat(taxCulture, "{0:C3}", taxationTradeReportItem.TaxationSellPrice)
                    .Append("\t|")
                    .AppendFormat(culture, "{0:C3}", taxationTradeReportItem.SellFee)
                    .Append("\t|")
                    .AppendFormat(taxCulture, "{0:C3}", taxationTradeReportItem.TaxationSellFee)
                    .Append("\t|")
                    .AppendFormat(culture, "{0:C3}", taxationTradeReportItem.Result)
                    .Append("\t|")
                    .AppendFormat(taxCulture, "{0:C3}", taxationTradeReportItem.TaxationResult)
                    .Append("\t|")
                    .AppendFormat(taxCulture, "{0:C3}", taxationTradeReportItem.CalculatedTax);

                System.Console.WriteLine(sb);
            }
        }
    }
}
