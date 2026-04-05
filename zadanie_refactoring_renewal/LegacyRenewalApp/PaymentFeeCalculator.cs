namespace LegacyRenewalApp;

public class PaymentFeeCalculator
{
    IPaymentMethod  _paymentMethod;

    public PaymentFeeCalculator(IPaymentMethod paymentMethod)
    {
        _paymentMethod = paymentMethod;
    }

    public BasicResult Calculate(decimal amount)
    {
        return _paymentMethod.calculate(amount);
    }
}