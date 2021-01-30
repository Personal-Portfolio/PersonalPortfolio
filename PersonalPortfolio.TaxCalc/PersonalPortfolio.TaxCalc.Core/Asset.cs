using System;

namespace PersonalPortfolio.TaxCalc.Core
{
    public class Asset
    {
        public DateTime SettleDate { get; set; }

        public decimal Price { get; set; }

        public decimal Fee { get; set; }
    }
}
