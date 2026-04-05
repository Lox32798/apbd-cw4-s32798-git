namespace LegacyRenewalApp;

public class TaxCalculator : ITaxCalculator
{
    public decimal CalculateTax(ICountry country)
    {
        return country.GetTaxRate();
    }
}

public interface ITaxCalculator
{
    public decimal CalculateTax(ICountry country);
}