using System;
using System.Collections.Generic;
using System.Globalization;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.BrokerParsing
{
    internal class StockTradeItemFactory : ITradeItemFactory
    {
        // TODO: options
        private static readonly HashSet<string> AssetsWhiteList = new()
        {
            "STK"
        };

        public TradeItem Create(IDictionary<string, string> tradeRecord)
        {
            if (!tradeRecord.TryGetValue("AssetClass", out var assetClass))
                throw new ReportParsingException("Invalid value in \'AssetClass\' field.");

            if (!AssetsWhiteList.Contains(assetClass))
                return null;

            if (!tradeRecord.TryGetValue("SettleDateTarget", out var settleDateTargetString)
                || !DateTime.TryParseExact(
                    settleDateTargetString,
                    "yyyyMMdd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AdjustToUniversal,
                    out var settleDateTarget))
                throw new ReportParsingException("Invalid value in \'SettleDateTarget\' field.");

            if (!tradeRecord.TryGetValue("Symbol", out var symbol))
                throw new ReportParsingException("Invalid value in \'Symbol\' field.");

            if (!tradeRecord.TryGetValue("Quantity", out var quantity)
                || !int.TryParse(quantity, out var amount))
                throw new ReportParsingException("Invalid value in \'Quantity\' field.");

            if (!tradeRecord.TryGetValue("Proceeds", out var costString)
                || !decimal.TryParse(costString, out var cost))
                throw new ReportParsingException("Invalid value in \'Proceeds\' field.");

            if (!tradeRecord.TryGetValue("IBCommission", out var feeString)
                || !decimal.TryParse(feeString, out var fee))
                throw new ReportParsingException("Invalid value in \'IBCommission\' field.");

            if (!tradeRecord.TryGetValue("Buy/Sell", out var buySell)
                || !Enum.TryParse<TradeType>(buySell, true, out var tradeType))
                throw new ReportParsingException("Invalid value in \'Buy/Sell\' field.");

            if (!tradeRecord.TryGetValue("CurrencyPrimary", out var currencyString)
                || !Enum.TryParse<Currency>(currencyString, true, out var currency))
                throw new ReportParsingException("Invalid value in \'CurrencyPrimary\' field.");

            return new TradeItem(settleDateTarget, symbol, Math.Abs(amount), Math.Abs(cost), Math.Abs(fee), tradeType, currency);
        }
    }
}