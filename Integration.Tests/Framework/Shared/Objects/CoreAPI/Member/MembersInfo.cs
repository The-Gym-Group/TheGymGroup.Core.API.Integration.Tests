using Framework.Shared.Objects.CoreAPI.Account;

namespace Framework.Shared.Objects.CoreAPI.Member
{
    public class MembersInfo
    {
        public Guid MemberId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public string MemberStatus { get; set; }
        public Guid BranchId { get; set; }
        public Guid PrimaryAccountId { get; set; }
        public AccountDto PrimaryAccountDto { get; set; }
        public string MemberType { get; set; }
        public DateTime MemberAreaLastLogin { get; set; }
        public bool BouncedEmail { get; set; }
        public bool BouncedSms { get; set; }
        public int FreePtSessionsAvailable { get; set; }
        public bool HasExpiredDayPassAccount { get; set; }
        public bool HasPendingAccount { get; set; }
        public Guid[]? AddOnTemplateIds { get; set; }
        public bool AthletePerksOptIn { get; set; }
        public int FitQuestBCTokensRemaining { get; set; }
        public int FitQuestFQTokensRemaining { get; set; }
        public DateTime LoadDate { get; set; }
        public string Persona { get; set; }
        public string AuthToken { get; set; }
        public DateTime AuthTokenExpiry{get;set;}
        public bool AllowAccessToBaf { get; set; }
        public bool InductionCompleted { get; set; }
        public bool InductionRegularCompleted { get; set; }
        public bool InductionU18Completed { get; set; }
        public bool InductionDisabledCompleted { get; set; }
        public bool InGymInductionCompleted { get; set; }
        public bool IsUnder18 { get; set; }
    }
}
