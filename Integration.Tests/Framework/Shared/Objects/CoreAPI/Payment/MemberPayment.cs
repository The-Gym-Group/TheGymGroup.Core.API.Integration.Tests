namespace Framework.Shared.Objects.CoreAPI.Payment
{
    public class MemberPayment
    {
        public Guid BranchId { get; set; }
        public Guid MemberId { get; set; }
        public Guid AccountId { get; set; }
        public string Description { get; set; }
        public string Method { get; set; }
        public double Amount { get; set; }
        public DateTime ProcessedDate { get; set; }
        public string BillingReference { get; set; }
        public bool SendToBillingProvider { get; set; }
    }
}
