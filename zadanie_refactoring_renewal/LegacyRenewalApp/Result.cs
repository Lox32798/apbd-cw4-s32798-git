namespace LegacyRenewalApp;

public interface IResult
{
    public decimal Amount { get; }
    public string Note { get; }
}

public class BasicResult : IResult
{
    public decimal Amount { get; }
    public string Note { get; }

    public BasicResult(decimal amount, string note)
    {
        Amount = amount;
        Note = note;
    }
}