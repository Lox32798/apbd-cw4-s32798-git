using System;

namespace LegacyRenewalApp;

public class NormalizedFeeCalculator : INormalizedFeeCalculator
{
    public NormalizedFeeCalculator()
    {
    }
    public BasicResult Calculate(INormalizedFee normalizedFee, bool includePremiumSupport)
    {
        var note = String.Empty;
        if (includePremiumSupport)
        {
            note += "premium support included; ";
        }
        return new BasicResult(normalizedFee.calculatedFee(), note);
    }
}

public interface INormalizedFeeCalculator
{
    public BasicResult Calculate(INormalizedFee normalizedFee, bool includePremiumSupport);
}