using Microsoft.Extensions.DependencyInjection;
using Wikiled.Common.Utilities.Modules;

namespace Wikiled.Server.Core.Helpers
{
    public static class AsyncInitializationExtensions
    {
        public static IServiceCollection AddAsyncInitializer<T>(this IServiceCollection services)
            where T : class, IAsyncInitialiser
        {
            services.AddSingleton<T>();
            return services.AddAsyncFactory<T>((collection, service) => service.InitializeAsync());
        }
    }
}
