using Framework.Shared.Objects.CoreAPI.Account;
using Framework.Shared.Objects.CoreAPI.Branches;
using Framework.Shared.Objects.CoreAPI.Other;
using Framework.Shared.Objects.Templates;

namespace Framework.Shared.Objects.CoreAPI.Member
{
    public class MemberProfile
    {
        public string Gender { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string PinNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string TownOrCity { get; set; }
        public string Postcode { get; set; }
        public object HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public bool PhoneOrMobileOptIn { get; set; }
        public bool EmailOptIn { get; set; }
        public bool MailOptIn { get; set; }
        public bool? ThirdPartyPhoneOrMobileOptIn { get; set; }
        public bool? ThirdPartyEmailOptIn { get; set; }
        public bool? SecondaryGymOptIn { get; set; }
        public bool? TertiaryGymOptIn { get; set; }
        public bool? QuaternaryGymOptIn { get; set; }
        public bool? QuinaryGymOptIn { get; set; }
        public List<Branch> ReciprocalBranchOptIns { get; set; }
        public CarRegistration CarRegistration1 { get; set; }
        public CarRegistration CarRegistration2 { get; set; }
        public bool DisabledAccess { get; set; }
        public bool HasPin { get; set; }
        public bool AthletePerksOptIn { get; set; }
        public int FitQuestBCTokensRemaining { get; set; }
        public int FitQuestFQTokensRemaining { get; set; }
        public bool MemberHasFitQuestAccess { get; set; }
        public string ProspectType { get; set; }
        public Guid MemberId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public string MemberStatus { get; set; }
        public string BranchId { get; set; }
        public string PrimaryAccountId { get; set; }
        public AccountDto PrimaryAccountDto { get; set; }
        public string MemberType { get; set; }
        public DateTime MemberAreaLastLogin { get; set; }
        public bool BouncedEmail { get; set; }
        public bool BouncedSms { get; set; }
        public int FreePtSessionsAvailable { get; set; }
        public bool HasExpiredDayPassAccount { get; set; }
        public bool HasPendingAccount { get; set; }
        public List<AddOnTemplate> AddOnTemplateIds { get; set; }
        public DateTime LoadDate { get; set; }
        public string Persona { get; set; }
        public string AuthToken { get; set; }
        public DateTime AuthTokenExpiry { get; set; }
        public bool AllowAccessToBaf { get; set; }
        public bool InductionCompleted { get; set; }
        public bool InductionRegularCompleted { get; set; }
        public bool InductionU18Completed { get; set; }
        public bool InductionDisabledCompleted { get; set; }
        public bool InGymInductionCompleted { get; set; }
        public bool IsUnder18 { get; set; }
    }
}
