using System;

namespace PersonalPortfolio.Shared.Storage
{
    public class SymbolRate: Entity
    {
        public int TargetSymbolId { get; set; }
        public int SourceSymbolId { get; set; }
        public Security TargetSymbol { get; set; }
        public Security SourceSymbol { get; set; }
        public DateTime RateTime { get; set; }
        public float Value { get; set; }
    }
}
