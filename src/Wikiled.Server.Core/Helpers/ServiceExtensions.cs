using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Wikiled.Server.Core.Helpers
{
    public static class ServiceExtensions
    {
        public static TConfig RegisterConfiguration<TConfig>(this IServiceCollection services, IConfiguration configuration)
            where TConfig : class, new()
        {
            return services.RegisterConfiguration(configuration, new TConfig());
        }

        public static TConfig RegisterConfiguration<TConfig>(this IServiceCollection services, IConfiguration configuration, TConfig config) 
            where TConfig : class
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (config == null) throw new ArgumentNullException(nameof(config));

            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }
    }
}
