namespace PersonalPortfolio.Shared.Abstractions
{
    public interface IRate
    {
        string Code { get; }

        decimal Value { get; }

        int Size { get; }
    }
}