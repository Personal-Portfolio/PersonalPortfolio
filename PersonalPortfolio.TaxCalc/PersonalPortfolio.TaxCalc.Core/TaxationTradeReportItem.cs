using System;

namespace PersonalPortfolio.TaxCalc.Core
{
    public class TaxationTradeReportItem
    {
        public string AssetSymbol { get; set; }

        public DateTime AssetInputDate { get; set; }

        public DateTime AssetOutputDate { get; set; }

        public decimal BuyPrice { get; set; }

        public decimal SellPrice { get; set; }

        public decimal BuyFee { get; set; }

        public decimal SellFee { get; set; }

        public decimal Result { get; set; }

        public decimal TaxationBuyPrice { get; set; }

        public decimal TaxationSellPrice { get; set; }

        public decimal TaxationBuyFee { get; set; }

        public decimal TaxationSellFee { get; set; }

        public decimal TaxationResult { get; set; }

        public decimal CalculatedTax { get; set; }

        public Currency Currency { get; set; }
    }
}