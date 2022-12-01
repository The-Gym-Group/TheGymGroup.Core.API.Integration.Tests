using Flurl.Http;
using Flurl.Http.Configuration;
using Framework.Api.Base;
using Framework.Properties.Constants;
using IntegrationTests.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public class Tests
    {
        protected IServiceCollection Services = DependencyHelper.ResolveDependenciesBase();

        protected IFlurlClient Members;
        protected IFlurlClient Clubware;

        [SetUp]
        public async void Setup()
        {
            var factory = Services.GetService<IFlurlClientFactory>();
            Members = factory.Get(new Flurl.Url($"{ApiServerConfiguration.CoreAPI_URL}/member/"));
            Clubware = await factory.Get(new Flurl.Url($"{ApiServerConfiguration.CLUBWARE_URL}")).ConfigureForClubware();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}