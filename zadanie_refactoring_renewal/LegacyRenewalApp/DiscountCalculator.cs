namespace LegacyRenewalApp;

public class DiscountCalculator
{
    
    private readonly Customer _customer;
    private readonly SubscriptionPlan _plan;
    
    public DiscountCalculator(Customer customer, SubscriptionPlan plan)
    {
        
        _customer = customer;
        _plan = plan;
    }

    public BasicResult CalculateDiscount(decimal orderAmount)
    {
        decimal discountAmount = 0m;
        string notes = string.Empty;
        
        var loyalCalc = new LoyalityCalculator(_customer.YearsWithCompany);
        
        var discountSegment = _customer.Segment.CalculateDiscount(orderAmount,  _plan);
        var discountLoyality = loyalCalc.calculateDiscount(orderAmount);
        
        discountAmount = discountSegment.Amount +  discountLoyality.Amount;
        notes += discountSegment.Note + discountLoyality.Note;
        
        return new BasicResult(discountAmount, notes);
    }
}