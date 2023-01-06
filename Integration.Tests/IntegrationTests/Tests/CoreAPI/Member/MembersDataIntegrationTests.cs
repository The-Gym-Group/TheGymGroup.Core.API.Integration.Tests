using Flurl.Http;
using Framework.Api.Response.CoreAPI;
using Framework.Properties.Constants;
using Framework.Shared.Objects.Clubware;
using Framework.Shared.Objects.CoreAPI.Member;
using Framework.Shared.Objects.CoreAPI.Payment;
using IntegrationTests.Utils.Base;
using Microsoft.AspNetCore.Authentication;
using System.Net;
using TestStack.BDDfy;
using TestStack.BDDfy.Xunit;

namespace IntegrationTests.Tests.CoreAPI.Member
{
    [TestFixture]
    [Story(
        AsA = "As an API user",
        IWant = "I want to connect to Member API",
        SoThat = "I can query and update data in Clubware")]
    public class MembersDataIntegrationTests : BaseClubwareIntegrationTest
    {
        List<PartialClubwareMember> ClubwareMembers { get; set; }
        #region privateVars
        private List<PartialClubwareMember> random10cMembers;
        private IFlurlResponse[] random10MembersInfoResponses;
        private List<MemberInfo> random10MembersInfo;
        private MemberProfile[]? random10MembersProfiles;
        private PartialClubwareMember partialMember;
        private CoreApiResponse<List<MemberPayment>> memberPaymentHistory;
        private MemberProfile memberProfile;
        private IFlurlResponse memberProfileResponse;
        #endregion


        [Test]
        public async Task GetMemberPaymentHistory_ReturnsData()
        {
            this.Given(_ => IGet10RandomClubwareMembers())
                .And(_ => IGetARandomCMember())
                .When(_ => IExtractMemberPaymentHistory())
                .Then(_ => PaymentHistoryHasValidDate())
                .BDDfy();
        }

        [Test]
        public async Task GetMemberProfileById_ReturnsData()
        {
            this.Given(_ => IGet10RandomClubwareMembers())
                .And(_ => IGetARandomCMember())
                .When(_ => IExtractMemberProfileFromCMember()).
                Then(_ => MemberProfileHasValidData()).BDDfy();

        }

        [Test]
        public async Task GetMembersProfileById_Using10RandomIds_GetsRelevantData()
        {
            this.Given(_ => IGet10RandomClubwareMembers())
                .Given(_ => IHave10RandomPartialCMembers())
                .When(_ => IExtractMemberProfilesFromPartialCMemberData())
                .Then(_ => MemberProfileDataMatchesClubwareData()).BDDfy();
        }

        [Test]
        public async Task GetMembersInfoByMembersId_Using10RandomIds_GetsRelevantData()
        {
            this.Given(_ => IGet10RandomClubwareMembers())
                .Given(_ => IHave10RandomPartialCMembers())
                .When(_ => IExtractMembersInfoResponsesFromPartialCMembers())
                .And(_ => IGetMembersInfoFromResponses())
                .Then(_ => MembersInfoMatchClubwareMemberData()).BDDfy();
        }

        private void IHaveAnInvalidCMember()
        {
            partialMember = new PartialClubwareMember()
            {
                MemberId = Guid.Parse(TestDataConstants.NO_DATA_GUID_FORMAT)
            };
        }
        private void MembersInfoMatchClubwareMemberData()
        {
            Assert.Multiple(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    var info = random10MembersInfo[i];
                    var cMember = await GetClubMemberFromPartial(random10cMembers[i]);

                    Assert.NotNull(info, $"No MemberInfo found for clubware MemberId [{cMember.MemberId}]");
                    if (random10MembersInfoResponses[i].StatusCode == (int)HttpStatusCode.OK)
                        if (info != null)
                        {
                            Assert.NotNull(info.FirstName, $"[{i + 1}/{random10cMembers.Count}]Expected Member info for clubware MemberId [{cMember.MemberId}] to not be null, but it was null.");
                            Assert.That(info.MemberId.ToString(), Is.EqualTo(cMember.MemberId.ToString()), $"[{i + 1}/{random10cMembers.Count}]MemberId does not match. This shouldn't be possible. Expected [{cMember.MemberId}] but got [{info.MemberId}] for Cluware MemberId {cMember.MemberId}.");
                            Assert.That(info.FirstName, Is.EqualTo(cMember.FirstName), $"[{i + 1}/{random10cMembers.Count}]FirstName does not match. Expected [{cMember.FirstName}] but got [{info.FirstName}] for Cluware MemberId {cMember.MemberId}.");
                            Assert.That(info.PrimaryAccountDto, Is.Not.Null, $"[{i + 1}/{random10cMembers.Count}]Expected Member info to have a primary account dto assigned but found null for ClubwareMemberId {cMember.MemberId}.");
                            Assert.That(info.BranchId.ToString(), Is.EqualTo(cMember.BranchId.ToString()));
                            Assert.That(info.Status.ToString(), Is.EqualTo(cMember.Status.ToString()));
                        }
                }
            });
        }

        private async Task IGetMembersInfoFromResponses()
        {
            random10MembersInfo = (await Task.WhenAll(random10MembersInfoResponses.Select(async response => await response.GetJsonAsync<CoreApiResponse<MemberInfo>>()))).Select(response => response.Data).ToList();
        }

        private async Task IExtractMembersInfoResponsesFromPartialCMembers()
        {
            random10MembersInfoResponses = await Task.WhenAll(random10cMembers
               .Select(async cMember =>
                   await $"{Members.BaseUrl}members/member/{cMember.MemberId.ToString()}"
                   .WithClient(Members).GetAsync()));
        }

        private void MemberProfileDataMatchesClubwareData()
        {
            Assert.Multiple(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    var profile = random10MembersProfiles[i];
                    var cMember = await GetClubMemberFromPartial(random10cMembers[i]);

                    Console.WriteLine($"Checking member [{i + 1}/{random10cMembers.Count}]. MemberId:[{cMember?.MemberId}]");
                    Assert.NotNull(profile);
                    if (profile != null)
                    {
                        Assert.NotNull(profile.FirstName, $"[{i + 1}/{random10cMembers.Count}]Expected Member info for clubware MemberId [{cMember.MemberId}] to not be null, but it was null.");
                        Assert.That(profile.MemberId.ToString(), Is.EqualTo(cMember.MemberId.ToString()), $"[{i + 1}/{random10cMembers.Count}]MemberId does not match. This shouldn't be possible. Expected [{cMember.MemberId}] but got [{profile.MemberId}] for Cluware MemberId {cMember.MemberId}.");
                        Assert.That(profile.FirstName, Is.EqualTo(cMember.FirstName), $"[{i + 1}/{random10cMembers.Count}]FirstName does not match. Expected [{cMember.FirstName}] but got [{profile.FirstName}] for Cluware MemberId {cMember.MemberId}.");
                        Assert.That(profile.LastName, Is.EqualTo(cMember.LastName), $"[{i + 1}/{random10cMembers.Count}]LastName does not match. Expected [{cMember.FirstName}] but got [{profile.FirstName}] for Cluware MemberId {cMember.MemberId}.");
                        Assert.That(profile.PrimaryAccountDto, Is.Not.Null, $"[{i + 1}/{random10cMembers.Count}]Expected Member Profile to have a primary account dto assigned but found null for ClubwareMemberId {cMember.MemberId}.");
                        Assert.That(profile.PrimaryAccountDto.AccountId, Is.EqualTo(cMember.PrimaryAccountId), $"[{i + 1}/{random10cMembers.Count}]Expected Member Profile to have a primary account dto equal to clubware for ClubwareMemberId {cMember.MemberId}.");
                        Assert.That(profile.HasPin, Is.EqualTo(!string.IsNullOrEmpty(cMember.PinNumber)), $"Expected that since Clubware MemberId '{cMember.MemberId}' PinNumber is '{cMember.PinNumber}', profile.HasPin to be '{profile.HasPin}'");
                        Assert.That(profile.PinNumber, Is.EqualTo(cMember.PinNumber), $"Expected that since Clubware PinNumber is '{cMember.PinNumber}', profile.PinNumber to be '{profile.PinNumber}'");
                        Assert.That(profile.Email, Is.EqualTo(cMember.Email));
                    }
                }
            });
        }

        private async Task IExtractMemberProfilesFromPartialCMemberData()
        {
            random10MembersProfiles = await Task.WhenAll(random10cMembers
                            .Select(async cMember =>
                                await $"{Members.BaseUrl}members/profile/{cMember.MemberId}"
                                .WithClient(Members).GetAsync()
                                .ContinueWith(response => response.Result.GetJsonAsync<CoreApiResponse<MemberProfile>>().Result.Data)));
            if (random10MembersProfiles == null) Assert.Fail("Could not get any member profiles for cmembers.");
        }


        public async Task IGet10RandomClubwareMembers()
        {
            if (ClubwareMembers == null || ClubwareMembers.Count == 0)
            {
                var responses = await $"{Clubware.BaseUrl}/members"
                .WithClient(Clubware)
                .GetAsync();
                ClubwareMembers = await responses.GetJsonAsync<List<PartialClubwareMember>>();
            }
        }
        private async Task IExtractMemberProfileFromCMember()
        {
            memberProfileResponse = await $"{Members.BaseUrl}members/profile/{partialMember.MemberId.ToString()}"
                    .WithClient(Members).GetAsync();
            memberProfile = (await memberProfileResponse.GetJsonAsync<CoreApiResponse<MemberProfile>>()).Data;

        }

        private void PaymentHistoryHasValidDate()
        {
            if (memberPaymentHistory.Data.Count > 0)
                Assert.Multiple(() =>
                {
                    int i = 0;
                    foreach (var paymentHistory in memberPaymentHistory.Data)
                    {
                        i++;
                        AssertPaymentHistoryHasValidData(partialMember, memberPaymentHistory, i, paymentHistory);
                    }
                });
        }

        private async Task IExtractMemberPaymentHistory()
        {
            var memberPaymentHistoryResponse = await $"{Members.BaseUrl}members/payments/{partialMember.MemberId.ToString()}"
                    .WithClient(Members).GetAsync();
            memberPaymentHistory = await memberPaymentHistoryResponse.GetJsonAsync<CoreApiResponse<List<MemberPayment>>>();

            Assert.That(memberPaymentHistory.Success, Is.True);
            Assert.That(memberPaymentHistoryResponse.StatusCode, Is.EqualTo(200));
        }

        private void MemberProfileHasValidData()
        {
            Assert.That(memberProfileResponse.StatusCode == 200);
            Assert.IsNotNull(memberProfile);
            Assert.That(memberProfile.FirstName, Is.Not.Null);
            Assert.That(memberProfile.FirstName.Length, Is.GreaterThan(0));
        }

        private static void AssertPaymentHistoryHasValidData(PartialClubwareMember partialMember, CoreApiResponse<List<MemberPayment>> memberPaymentHistory, int i, MemberPayment paymentHistory)
        {
            Assert.That(paymentHistory.MemberId.ToString(), Is.Not.EqualTo(TestDataConstants.NO_DATA_GUID_FORMAT), $"No valid MemberId found in PaymentHistory {i / memberPaymentHistory.Data.Count} for id '{partialMember.MemberId}'");
            Assert.That(paymentHistory.BranchId.ToString(), Is.Not.EqualTo(TestDataConstants.NO_DATA_GUID_FORMAT), $"No valid BranchId found in PaymentHistory {i / memberPaymentHistory.Data.Count} for id '{partialMember.MemberId}'");
            Assert.That(paymentHistory.AccountId.ToString(), Is.Not.EqualTo(TestDataConstants.NO_DATA_GUID_FORMAT), $"No valid AccountId found in PaymentHistory {i / memberPaymentHistory.Data.Count} for id '{partialMember.MemberId}'");
            Assert.That(paymentHistory.Method.ToString(), Is.Not.Null, $"No valid Method string found in PaymentHistory {i / memberPaymentHistory.Data.Count} for id '{partialMember.MemberId}'");
            Assert.That(paymentHistory.Method.ToString(), Is.Not.Empty, $"No valid Method string found in PaymentHistory {i / memberPaymentHistory.Data.Count} for id '{partialMember.MemberId}'");
        }

        private void IGetARandomCMember()
        {
            partialMember = ClubwareMembers[new Random().Next(ClubwareMembers.Count - 1)];
        }

        private async Task<ClubwareMember> GetClubMemberFromPartial(PartialClubwareMember partialMember)
        {
            return await await $"{Clubware.BaseUrl}/members/{partialMember.MemberId}"
            .WithClient(Clubware).GetAsync()
            .ContinueWith(async response =>
                await response.Result.GetJsonAsync<ClubwareMember>());
        }

        private void IHave10RandomPartialCMembers()
        {
            int i = new Random().Next(0, ClubwareMembers.Count - 11);
            random10cMembers = ClubwareMembers.GetRange(i, 10);
        }
    }
}
