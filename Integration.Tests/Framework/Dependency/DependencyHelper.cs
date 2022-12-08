using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Framework.Dependency
{
    public static class DependencyHelper
    {
        public static IServiceCollection ResolveDependenciesBase()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IFlurlClientFactory, DefaultFlurlClientFactory>();
            services.TryAddTransient<IFlurlClient, FlurlClient>();

            return services;
        }

        public static T GetService<T>(this IServiceCollection services) where T: class
        {
            try
            {
                var type = typeof(T);

#if DEBUG
                Console.WriteLine($"T is of type {type.FullName}");
#endif
                var scopedService = ResolveDependenciesBase().BuildServiceProvider().GetService(type);
                return scopedService as T;
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldn't get required service:" + e);
                throw;
            }
        }
    }
}
