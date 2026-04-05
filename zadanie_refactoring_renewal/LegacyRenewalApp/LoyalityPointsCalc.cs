namespace LegacyRenewalApp;

public class LoyalityPointsCalc
{
    private int _loyaltyPoints;
    private bool _pointsToUse;
    public LoyalityPointsCalc(int loyaltyPoints, bool pointsToUse)
    {
        
    }

    public BasicResult CalculateDiscount()
    {
        string notes = string.Empty;
        int pointsToUse = 0;
        if (_pointsToUse && _loyaltyPoints > 0)
        {
            pointsToUse = _loyaltyPoints > 200 ? 200 : _loyaltyPoints;
            notes += $"loyalty points used: {pointsToUse}; ";
        }
        return new BasicResult(pointsToUse, notes);
    }
}