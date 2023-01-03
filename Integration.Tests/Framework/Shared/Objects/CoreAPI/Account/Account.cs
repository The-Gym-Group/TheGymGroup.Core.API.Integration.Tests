using Framework.Shared.Objects.CoreAPI.Branches;

namespace Framework.Shared.Objects.CoreAPI.Account
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public List<Branch> ReciprocalAccessBranches { get; set; }
        public int NumberOfAdditionalBranches { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsPending9000State { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public object BillingReference { get; set; }
        public DateTime NextBillingDate { get; set; }
        public DateTime LoadDate { get; set; }
        public string TggMembershipType { get; set; }
        public string Description { get; set; }
        public double ExpectedPaymentAmount { get; set; }
        public int Term { get; set; }
        public string TermType { get; set; }
        public string AccountTemplateId { get; set; }
        public object BlockOrWatchResult { get; set; }
        public DateTime LastAccountChange { get; set; }
        public DateTime NextAccountChange { get; set; }
        public double RolloverFixedTermFee { get; set; }
        public object RolloverFixedTermDurationApplied { get; set; }
        public bool ReciprocalBranchAccessAllowed { get; set; }
        public bool WebBranchAccessOverride { get; set; }
        public bool FreeFiiTEnabled { get; set; }
        public int? KickerPriceDuration { get; set; }
        public double? KickerPriceMonthlyFee { get; set; }
        public bool IsLastPaymentKickerPrice { get; set; }
        public bool IsNextPaymentKickerPrice { get; set; }
        public double LastPaymentAmount { get; set; }
        public double NextPaymentAmount { get; set; }
    }
}
