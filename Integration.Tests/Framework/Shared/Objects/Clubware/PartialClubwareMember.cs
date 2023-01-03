namespace Framework.Shared.Objects.Clubware
{
    public class PartialClubwareMember
    {
        public bool IsDeleted { get; set; }
        public Guid MemberId { get; set; }
        public Guid BranchId { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string PinNumber { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Address> Addresses { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
        public List<UserDefinedFieldValue> UserDefinedFieldValues { get; set; }

    }
}
