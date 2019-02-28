using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWebApi.Configuration
{
    /// <summary>
    /// Class Di.
    /// </summary>
    public static class Di
    {
        /// <summary>
        /// Configures the dependency injection.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The service provider.</returns>
        public static IServiceProvider ConfigureDependencyInjection(this IServiceCollection services,
            IConfiguration configuration)
        {
            // register dependencies
            services.AddSingleton(configuration);            
            services.AddMemoryCache();            

            return services.BuildServiceProvider();
        }
    }
}