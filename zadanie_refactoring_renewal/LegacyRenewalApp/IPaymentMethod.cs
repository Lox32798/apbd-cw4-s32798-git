using System;
namespace LegacyRenewalApp;

public interface IPaymentMethod
{
    public BasicResult calculate(decimal amount);
}

public class CardMethod :  IPaymentMethod
{
    public BasicResult calculate(decimal amount)
    {
        return new BasicResult(amount * 0.02m, "card payment fee; ");
    }
}

public class BankTransferMethod:  IPaymentMethod
{
    public BasicResult calculate(decimal amount)
    {
        return new BasicResult(amount * 0.01m, "bank transfer fee; ");
    }
}
public class PayPalMethod:  IPaymentMethod
{
    public BasicResult calculate(decimal amount)
    {
        return new BasicResult(amount * 0.035m, "paypal fee; ");
    }
}
public class InvoiceMethod:  IPaymentMethod
{
    public BasicResult calculate(decimal amount)
    {
        return new BasicResult(amount * 0.0m, "invoice payment; ");
    }
}

public class UnknownMethod:  IPaymentMethod
{
    public BasicResult calculate(decimal amount)
    {
        throw new ArgumentException("Unsupported payment method");
    }
}