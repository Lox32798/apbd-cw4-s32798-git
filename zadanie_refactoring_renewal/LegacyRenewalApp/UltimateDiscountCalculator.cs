using System;

namespace LegacyRenewalApp;

public class UltimateDiscountCalculator
{
    private readonly Customer _customer;
    private readonly SubscriptionPlan  _plan;
    private readonly int _seatCount;
    private readonly bool _useLoyaltyPoints;
    public UltimateDiscountCalculator(Customer customer, SubscriptionPlan plan, int seatCount, bool useLoyaltyPoints)
    {
        _customer = customer;
        _plan = plan;
        _seatCount = seatCount;
        _useLoyaltyPoints = useLoyaltyPoints;
    }
    public BasicResult CalculateDiscount(decimal baseAmount)
    {
        decimal discountAmount = 0m;
        string notes = string.Empty;
        
        var discountCalculator = new DiscountCalculator(_customer, _plan);
        var discountRes = discountCalculator.CalculateDiscount(baseAmount);
        discountAmount += discountRes.Amount;
        notes += discountRes.Note;

        var seatDiscCalc = new SeatDiscount(_seatCount);
        var discountSeatRes = seatDiscCalc.calculateDiscount(baseAmount);
        discountAmount += discountSeatRes.Amount;
        notes += discountSeatRes.Note;

        var pointsCalc = new LoyalityPointsCalc(_customer.LoyaltyPoints, _useLoyaltyPoints);
        var discountPoints = pointsCalc.CalculateDiscount();
        discountAmount += discountPoints.Amount;
        notes += discountPoints.Note;


        return new BasicResult(discountAmount, notes);
    }
}