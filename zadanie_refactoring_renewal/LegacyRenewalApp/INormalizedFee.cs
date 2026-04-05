namespace LegacyRenewalApp;

public interface INormalizedFee
{
    public decimal calculatedFee();
}

public class StartFee : INormalizedFee
{
    private readonly decimal fee = 250m;
    public decimal calculatedFee()
    {
        return fee;
    }
}

public class ProFee : INormalizedFee
{
    private readonly decimal fee = 400m;

    public decimal calculatedFee()
    {
        return fee;
    }
}
public class EnterpriseFee : INormalizedFee
{
    private readonly decimal fee = 700m;

    public decimal calculatedFee()
    {
        return fee;
    }
}

public class NoFee : INormalizedFee
{
    private readonly decimal fee = 700m;

    public decimal calculatedFee()
    {
        return fee;
    }
}