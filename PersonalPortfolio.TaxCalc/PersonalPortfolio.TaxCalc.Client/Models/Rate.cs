using PersonalPortfolio.Shared.Abstractions;

namespace PersonalPortfolio.TaxCalc.Client.Models
{
    internal sealed class Rate : IRate
    {
        public string Code { get; set; }
        public decimal Value { get; set;}
        public int Size { get; set;}
    }
}
