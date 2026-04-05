namespace LegacyRenewalApp;

public class PaymentMatcher : IPaymentMatcher{
    public PaymentMatcher()
    {
    }

    public IPaymentMethod Match(string normalizedPaymentMethod)
    {
        if (normalizedPaymentMethod == "CARD")
        {
            return new CardMethod();
        }
        else if (normalizedPaymentMethod == "BANK_TRANSFER")
        {
            return new BankTransferMethod();
        }
        else if (normalizedPaymentMethod == "PAYPAL")
        {
            return new PayPalMethod();
        }
        else if (normalizedPaymentMethod == "INVOICE")
        {
            return new InvoiceMethod();
        }
        else
        {
            return new UnknownMethod();
        }
    }
}

public interface IPaymentMatcher
{
    public IPaymentMethod Match(string normalizedPaymentMethod);
}