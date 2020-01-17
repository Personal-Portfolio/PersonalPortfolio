using System;

namespace PersonalPortfolio.Shared.Storage
{
    public class SecurityPrice: Entity
    {
        public int SecurityId { get; set; }
        public Security Security { get; set; }

        public DateTime TradeDate { get; set; }

        public decimal Average { get; set; }

        public decimal Open { get; set; }

        public decimal Close { get; set; }

        public decimal Low { get; set; }

        public decimal High { get; set; }
    }
}