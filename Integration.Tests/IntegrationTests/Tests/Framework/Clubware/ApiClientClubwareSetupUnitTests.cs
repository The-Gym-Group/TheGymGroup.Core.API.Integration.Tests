using Flurl.Http;
using Framework.Shared.Objects.Clubware;
using IntegrationTests.Utils.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Tests.Framework.Clubware
{
    [TestFixture]
    public class ApiClientClubwareSetupUnitTests : BaseClubwareIntegrationTest
    {
        [Test]
        public void ClubwareIntegrationSetup_HasAccessTokenHeader()
        {
            Assert.That(Clubware.Headers.Any(header => header.Name == "AccessToken"));
        }

        [Test]
        public void ClubwareIntegrationSetup_HasAccessTokenValue()
        {
            var (Name, Value) = Clubware.Headers.FirstOrDefault(header => header.Name == "AccessToken");
            Assert.That(Value, Is.Not.Null);
            Assert.That(Value.Length, Is.AtLeast(80));
            Console.WriteLine($"{Name}: {Value}");
        }

        [Test]
        public async Task ClubwareIntegrationSetup_Members_CallingGetMemberReturnsData()
        {
            var url = $"{Clubware.BaseUrl}/members/520c6a6e-5852-4dbc-8a3f-c6edde2b86d7";
            var getMemberResponse = await url.WithClient(Clubware).GetAsync();

            Assert.That(getMemberResponse.StatusCode, Is.EqualTo(200));
            var content = await getMemberResponse.GetJsonAsync<ClubwareMember>();
            Assert.That(content, Is.Not.Null);
            Assert.That(content.MemberId.ToString(), Is.EqualTo("520c6a6e-5852-4dbc-8a3f-c6edde2b86d7"));
        }
    }
}
