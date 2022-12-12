namespace Framework.Shared.Objects.Clubware
{
    public class ClubwareMember
    {

        public Guid MemberId { get; set; }
        public Guid BranchId { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string Alias1 { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public bool DoNotCall { get; set; }
        public bool DoNotSms { get; set; }
        public bool DoNotMail { get; set; }
        public bool DoNotEmail { get; set; }
        public string PinNumber { get; set; }
        public DateTime LoadDate { get; set; }
        public double RewardPoints { get; set; }
        public string PrimaryAccountId { get; set; }
        public string Title { get; set; }
        public bool DisabledAccess { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

    }
}
