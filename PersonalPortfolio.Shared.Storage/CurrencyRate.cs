using System;

namespace PersonalPortfolio.Shared.Storage
{
    public class CurrencyRate: CurrencyLinkedEntity
    {
        public DateTime RateTime { get; set; }
        public decimal Value { get; set; }
        public int SourceCurrencyId { get; set; }
        public Currency SourceCurrency { get; set; }
        public int DataSourceId { get; set; }
    }
}