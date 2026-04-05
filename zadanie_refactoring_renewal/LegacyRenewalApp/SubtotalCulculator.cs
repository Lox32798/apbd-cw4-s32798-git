using System;

namespace LegacyRenewalApp;

public class SubtotalCulculator : ISubtotalCalculator
{
    public BasicResult CalculateSubTotal(decimal amount, decimal discount)
    {
        var notes = String.Empty;
        var subtotalAfterDiscount = amount - discount;
        if (subtotalAfterDiscount < 300m)
        {
            subtotalAfterDiscount = 300m;
            notes += "minimum discounted subtotal applied; ";
        }
        return new BasicResult(subtotalAfterDiscount, notes);
    }
}

public interface ISubtotalCalculator
{
    BasicResult CalculateSubTotal(decimal amount, decimal discount);
}