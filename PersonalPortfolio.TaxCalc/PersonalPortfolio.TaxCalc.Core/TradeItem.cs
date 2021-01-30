using System;

namespace PersonalPortfolio.TaxCalc.Core
{
    public class TradeItem
    {
        public TradeItem(DateTime tradeDate, string symbol, int amount, decimal cost, decimal fee, TradeType type, Currency currency)
        {
            TradeDate = tradeDate;
            Symbol = symbol;
            Amount = amount;
            Cost = cost;
            Fee = fee;
            Type = type;
            Currency = currency;
        }

        public DateTime TradeDate { get; }

        public string Symbol { get; }

        public int Amount { get; }

        public decimal Cost { get; }

        public decimal Fee { get; }

        public TradeType Type { get; }

        public Currency Currency { get; }

        public static bool operator ==(TradeItem left, TradeItem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TradeItem left, TradeItem right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(TradeDate);
            hashCode.Add(Symbol, StringComparer.OrdinalIgnoreCase);
            hashCode.Add(Amount);
            hashCode.Add(Cost);
            hashCode.Add(Fee);
            hashCode.Add((int) Type);
            hashCode.Add((int) Currency);
            return hashCode.ToHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType()
                   && Equals((TradeItem) obj);
        }

        protected bool Equals(TradeItem other)
        {
            return TradeDate.Equals(other.TradeDate)
                   && string.Equals(Symbol, other.Symbol, StringComparison.OrdinalIgnoreCase)
                   && Amount == other.Amount
                   && Cost == other.Cost
                   && Fee == other.Fee
                   && Type == other.Type
                   && Currency == other.Currency;
        }
    }
}