namespace LegacyRenewalApp;

public interface ICountry
{
    public decimal GetTaxRate();
}

public class Poland : ICountry
{
    public decimal GetTaxRate()
    {
        return 0.23m;
    }
}

public class Germany : ICountry
{
    public decimal GetTaxRate()
    {
        return 0.19m;
    }
}
public class Czech : ICountry
{
    public decimal GetTaxRate()
    {
        return 0.21m;
    }
}
public class Norway : ICountry
{
    public decimal GetTaxRate()
    {
        return 0.25m;
    }
}

public class NoCountryForOldMan : ICountry
{
    public decimal GetTaxRate()
    {
        return 0.2m;
    }
}