namespace PersonalPortfolio.Shared.Storage
{
    public abstract class CurrencyLinkedEntity : Entity
    {
        public int CurrencyId { get; set; }
        public Currency Currency{ get; set; }
    }
}