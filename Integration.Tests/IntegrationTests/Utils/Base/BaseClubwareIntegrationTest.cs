using Flurl.Http;
using Flurl.Http.Configuration;
using Framework.Api.Base;
using Framework.Properties.Constants;
using Framework.Dependency;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace IntegrationTests.Utils.Base
{
    [TestFixture]
    public class BaseClubwareIntegrationTest
    {
        protected IServiceCollection Services { get; set; } = DependencyHelper.ResolveDependenciesBase();
        protected IFlurlClient Members { get; set; }
        protected IFlurlClient Clubware { get; set; }

        [OneTimeSetUp]
        public async Task SetUp()
        {
            SetUpConfiguration();
            var factory = Services.GetService<IFlurlClientFactory>();
            Members = factory.Get(new Flurl.Url($"{ApiServerConfiguration.CoreAPI_URL}/member/"));
            Clubware = await factory.Get(new Flurl.Url($"{ApiServerConfiguration.CLUBWARE_URL}")).ConfigureForClubware(ApiServerConfiguration.CLUBWARE_URL);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Members.Dispose();
            Clubware.Dispose();
        }

        private void SetUpConfiguration()
        {
            var builder = WebApplication.CreateBuilder();
            #region FlurlDependencies
            builder.Services.AddSingleton<IFlurlClientFactory, DefaultFlurlClientFactory>();
            #endregion
            var app = builder.Build();
            ApiServerConfiguration.ROOT_URL = builder.Configuration["Configuration:ROOT_URL"];
            ApiServerConfiguration.ENVIRONMENT = builder.Configuration["Configuration:ENVIRONMENT"];
            ApiServerConfiguration.ConfigurationManager = builder.Configuration;

        }

    }
}