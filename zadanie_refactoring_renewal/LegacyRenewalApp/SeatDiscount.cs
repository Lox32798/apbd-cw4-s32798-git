namespace LegacyRenewalApp;

public class SeatDiscount
{
    private readonly int _seatCount;

    public SeatDiscount(int seatCount)
    {
        _seatCount = seatCount;
    }
    public BasicResult calculateDiscount(decimal baseAmount)
    {
        decimal discountAmount = 0m;
        string notes = string.Empty;
        if (_seatCount >= 50)
        {
            discountAmount += baseAmount * 0.12m;
            notes += "large team discount; ";
        }
        else if (_seatCount >= 20)
        {
            discountAmount += baseAmount * 0.08m;
            notes += "medium team discount; ";
        }
        else if (_seatCount >= 10)
        {
            discountAmount += baseAmount * 0.04m;
            notes += "small team discount; ";
        }
        return new BasicResult(discountAmount, notes);
    }
}