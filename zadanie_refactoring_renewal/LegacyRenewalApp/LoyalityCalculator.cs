namespace LegacyRenewalApp;

public class LoyalityCalculator
{
    readonly int _yearsWithCompany;

    public LoyalityCalculator(int yearsWithCompany)
    {
        _yearsWithCompany = yearsWithCompany;
    }
    public BasicResult calculateDiscount(decimal baseAmount)
    {
        decimal discountAmount = 0m;
        string notes = string.Empty;
        if (_yearsWithCompany >= 5)
        {
            discountAmount += baseAmount * 0.07m;
            notes += "long-term loyalty discount; ";
        }
        else if (_yearsWithCompany >= 2)
        {
            discountAmount += baseAmount * 0.03m;
            notes += "basic loyalty discount; ";
        }
        return new BasicResult(discountAmount, notes);
    }
}