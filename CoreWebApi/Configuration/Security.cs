using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWebApi.Configuration
{
    /// <summary>
    /// Authentication configuration
    /// </summary>
    public static class Security
    {
        /// <summary>
        /// Configures the HTTPS protocol.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="environment">The environment.</param>
        /// <returns></returns>
        public static IServiceCollection ConfigureHttps(this IServiceCollection services, IHostingEnvironment environment)
        {
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status301MovedPermanently;
                options.HttpsPort = 443;
            });

            return services;
        }

        /// <summary>
        /// Uses the HTTPS protocol.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="environment">The environment.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseHttps(this IApplicationBuilder app, IHostingEnvironment environment)
        {
            app.UseHttpsRedirection();

            return app;
        }
    }
}