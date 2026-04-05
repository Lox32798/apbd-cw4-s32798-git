namespace LegacyRenewalApp
{
    public class SubscriptionPlan
    {
        public INormalizedFee Code { get; set; } = new NoFee();
        public string Name { get; set; } = string.Empty;
        public decimal MonthlyPricePerSeat { get; set; }
        public decimal SetupFee { get; set; }
        public bool IsEducationEligible { get; set; }
    }
}
