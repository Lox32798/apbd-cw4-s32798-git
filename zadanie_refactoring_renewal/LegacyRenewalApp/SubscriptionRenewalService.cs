using System;

namespace LegacyRenewalApp
{
    public class SubscriptionRenewalService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
        /////////////////////////////////////////////////////////////////// 
        private readonly ISubtotalCalculator _subtotalCalculator;
        private readonly INormalizedFeeCalculator _feeCalculator;
        private readonly IPaymentMatcher _paymentMatcher;
        private readonly ITaxCalculator _taxCalculator;
        public SubscriptionRenewalService()
        {
            _customerRepository =  new CustomerRepository();
            _subscriptionPlanRepository = new SubscriptionPlanRepository();
            _subtotalCalculator = new SubtotalCulculator();
            _feeCalculator = new NormalizedFeeCalculator();
            _paymentMatcher =  new PaymentMatcher();
            _taxCalculator = new TaxCalculator();
        }
        public RenewalInvoice CreateRenewalInvoice(
            int customerId,
            string planCode,
            int seatCount,
            string paymentMethod,
            bool includePremiumSupport,
            bool useLoyaltyPoints)
        {
            ValidateData(customerId, planCode, seatCount, paymentMethod);
            
            string normalizedPlanCode = planCode.Trim().ToUpperInvariant();
            string normalizedPaymentMethod = paymentMethod.Trim().ToUpperInvariant();
            
            var customer = _customerRepository.GetById(customerId);
            var plan = _subscriptionPlanRepository.GetByCode(normalizedPlanCode);

            if (!customer.IsActive)
            {
                throw new InvalidOperationException("Inactive customers cannot renew subscriptions");
            }

            decimal baseAmount = (plan.MonthlyPricePerSeat * seatCount * 12m) + plan.SetupFee;

            string notes = string.Empty;
            decimal discountAmount = 0m;
            
            var ultimateDiscountCalc = new UltimateDiscountCalculator(customer, plan, seatCount, useLoyaltyPoints);
            var discountRes = ultimateDiscountCalc.CalculateDiscount(baseAmount);
            
            discountAmount += discountRes.Amount;
            notes += discountRes.Note;
            
            var sabTotalRes = _subtotalCalculator.CalculateSubTotal(baseAmount, discountAmount);
            decimal subtotalAfterDiscount = sabTotalRes.Amount;
            notes += sabTotalRes.Note;

            var normalizedFee = plan.Code;
            var feeResult = _feeCalculator.Calculate(normalizedFee, includePremiumSupport);
            
            decimal supportFee = feeResult.Amount;
            notes += feeResult.Note;

            var paymentMethodClass = _paymentMatcher.Match(normalizedPaymentMethod);
            var paymentCalc = new PaymentFeeCalculator(paymentMethodClass);
            var paymentRes = paymentCalc.Calculate(subtotalAfterDiscount + supportFee);
            decimal paymentFee = paymentRes.Amount;
            notes += paymentRes.Note;

            decimal taxRate = _taxCalculator.CalculateTax(customer.Country);
            
            decimal taxBase = subtotalAfterDiscount + supportFee + paymentFee;
            decimal taxAmount = taxBase * taxRate;
            decimal finalAmount = taxBase + taxAmount;

            if (finalAmount < 500m)
            {
                finalAmount = 500m;
                notes += "minimum invoice amount applied; ";
            }

            var invoice = new RenewalInvoice
            {
                InvoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd}-{customerId}-{normalizedPlanCode}",
                CustomerName = customer.FullName,
                PlanCode = normalizedPlanCode,
                PaymentMethod = normalizedPaymentMethod,
                SeatCount = seatCount,
                BaseAmount = Math.Round(baseAmount, 2, MidpointRounding.AwayFromZero),
                DiscountAmount = Math.Round(discountAmount, 2, MidpointRounding.AwayFromZero),
                SupportFee = Math.Round(supportFee, 2, MidpointRounding.AwayFromZero),
                PaymentFee = Math.Round(paymentFee, 2, MidpointRounding.AwayFromZero),
                TaxAmount = Math.Round(taxAmount, 2, MidpointRounding.AwayFromZero),
                FinalAmount = Math.Round(finalAmount, 2, MidpointRounding.AwayFromZero),
                Notes = notes.Trim(),
                GeneratedAt = DateTime.UtcNow
            };

            LegacyBillingGateway.SaveInvoice(invoice);

            if (!string.IsNullOrWhiteSpace(customer.Email))
            {
                string subject = "Subscription renewal invoice";
                string body =
                    $"Hello {customer.FullName}, your renewal for plan {normalizedPlanCode} " +
                    $"has been prepared. Final amount: {invoice.FinalAmount:F2}.";

                LegacyBillingGateway.SendEmail(customer.Email, subject, body);
            }

            return invoice;
        }

        private void ValidateData(
            int customerId,
            string planCode,
            int seatCount,
            string paymentMethod){
            if (customerId <= 0)
            {
                throw new ArgumentException("Customer id must be positive");
            }

            if (string.IsNullOrWhiteSpace(planCode))
            {
                throw new ArgumentException("Plan code is required");
            }

            if (seatCount <= 0)
            {
                throw new ArgumentException("Seat count must be positive");
            }

            if (string.IsNullOrWhiteSpace(paymentMethod))
            {
                throw new ArgumentException("Payment method is required");
            }
        }
    }
}
