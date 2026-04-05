namespace LegacyRenewalApp;

public interface IDiscountStrategy
{
    public BasicResult CalculateDiscount(decimal orderAmount, SubscriptionPlan subPlan);
}

public class SilverDiscount : IDiscountStrategy
{
    public BasicResult CalculateDiscount(decimal orderAmount, SubscriptionPlan subPlan)
    {
        return new BasicResult(orderAmount * 0.05m, "silver discount; ");
    }
}

public class GoldenDiscount : IDiscountStrategy
{
    public BasicResult CalculateDiscount(decimal orderAmount, SubscriptionPlan subPlan)
    {
        return new BasicResult(orderAmount * 0.1m,  "gold discount; ");
    }
}
public class PlatinumDiscount : IDiscountStrategy
{
    public BasicResult CalculateDiscount(decimal orderAmount, SubscriptionPlan subPlan)
    {
        return new BasicResult(orderAmount * 0.15m, "platinum discount; ");
    }
}

public class NoDiscount : IDiscountStrategy
{
    public BasicResult CalculateDiscount(decimal orderAmount, SubscriptionPlan subPlan)
    {
        return new BasicResult(0.0m, "");
    }
}

public class EducationDiscount : IDiscountStrategy
{
    public BasicResult CalculateDiscount(decimal orderAmount, SubscriptionPlan subPlan)
    {
        if(subPlan.IsEducationEligible)
         return new BasicResult(orderAmount * 0.2m, "education discount; ");
        
        return new BasicResult(0.0m, "");
    }
}