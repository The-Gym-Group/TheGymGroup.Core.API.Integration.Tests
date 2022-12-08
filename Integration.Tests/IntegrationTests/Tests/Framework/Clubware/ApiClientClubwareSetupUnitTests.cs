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
    }
}
