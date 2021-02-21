using System;
using PersonalPortfolio.Shared.Core;

namespace PersonalPortfolio.DataProviders.Moex.Model 
{
    public class Price : ISecurityPrice
    {
        public string CurrencyCode { get; set; }

        public string SecurityCode { get; set; }

        public DateTime TradeDate { get; set; }

        public decimal? Average { get; set; }

        public decimal? Open { get; set; }

        public decimal? Close { get; set; }

        public decimal? Low { get; set; }

        public decimal? High { get; set; }

        public string DataSourceCode { get; set; }
    }
}
