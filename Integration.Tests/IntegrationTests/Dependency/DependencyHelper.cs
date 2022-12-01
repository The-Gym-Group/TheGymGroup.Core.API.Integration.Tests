using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Flurl.Http.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Flurl.Http;

namespace IntegrationTests.Dependency
{
    internal static class DependencyHelper
    {
        public static IServiceCollection ResolveDependenciesBase()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IFlurlClientFactory, DefaultFlurlClientFactory>();
            services.TryAddTransient<IFlurlClient, FlurlClient>();

            return services;
        }

        public static T GetService<T>(this IServiceCollection services)
        {
            try
            {
                var scopedService = services.GetService<T>();
                return scopedService;
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldn't get required service:"+e);
                throw;
            }
        }
    }
}
