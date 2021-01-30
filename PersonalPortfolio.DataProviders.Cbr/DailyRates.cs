using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PersonalPortfolio.DataProviders.Cbr
{
    [CollectionDataContract(Name = "ValuteData", Namespace = "")]
    internal sealed class DailyRates: List<CbrRate>
    {
    }
}