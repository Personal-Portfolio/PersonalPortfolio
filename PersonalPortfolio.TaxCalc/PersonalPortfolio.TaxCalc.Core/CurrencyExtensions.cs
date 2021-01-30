using System;
using System.Globalization;

namespace PersonalPortfolio.TaxCalc.Core
{
    public static class CurrencyExtensions
    {
        public static CultureInfo ToCulture(this Currency currency)
        {
            var culture = currency switch
            {
                Currency.Usd => CultureInfo.CreateSpecificCulture("en-US"),
                Currency.Rub => CultureInfo.CreateSpecificCulture("ru-RU"),
                Currency.Eur => CultureInfo.CreateSpecificCulture("de-DE"),
                _ => throw new ArgumentOutOfRangeException(nameof(currency), currency, null)
            };

            culture.NumberFormat.CurrencyNegativePattern = 1;

            return culture;
        }
    }
}
