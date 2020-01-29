using System;

namespace PersonalPortfolio.Shared.Core
{
    public interface ISecurityPrice
    {
        string SecurityCode { get; set; }
        DateTime TradeDate { get; set; }
        decimal? Average { get; set; }
        decimal? Open { get; set; }
        decimal? Close { get; set; }
        decimal? Low { get; set; }
        decimal? High { get; set; }
        string CurrencyCode { get; set; }
        string DataSourceCode { get; set; }
    }
}
