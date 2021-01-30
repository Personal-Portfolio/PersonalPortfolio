using System.Runtime.Serialization;
using PersonalPortfolio.Shared.Abstractions;

namespace PersonalPortfolio.DataProviders.Cbr
{
    [DataContract(Name = "ValuteCursOnDate", Namespace = "")]
    internal sealed class CbrRate : IRate
    {
        [DataMember(Name = "Vnom", Order = 0)]
        public int Size { get; set; }

        [DataMember(Name = "Vcurs", Order = 1)]
        public decimal Value { get; set; }

        [DataMember(Name = "VchCode", Order = 2)]
        public string Code { get; set; }
    }
}

