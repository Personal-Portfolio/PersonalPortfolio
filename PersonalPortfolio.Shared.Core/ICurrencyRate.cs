using System;

namespace PersonalPortfolio.Shared.Core
{
    public interface ICurrencyRate
    {
        DateTime RateTime { get; set; }
        decimal Value { get; set; }
        string SourceCurrencyCode { get; set; }
        string CurrencyCode { get; set; }
        string DataSourceCode { get; set; }
    }
}
