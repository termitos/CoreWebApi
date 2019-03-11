using CoreWebApi.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWebApi.Configuration
{
    /// <summary>
    /// API configurations
    /// </summary>
    public static class Api
    {
        /// <summary>
        /// The default cors policy name
        /// </summary>
        public static string DefaultCorsPolicyName = "DefaultCorsPolicy";

        /// <summary>
        /// Configures the CORS policy.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(
                options =>
                {
                    options.AddPolicy(
                        DefaultCorsPolicyName,
                        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
                });
            return services;
        }

        /// <summary>
        /// Configures the MVC.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalActionFilter>();
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            return services;
        }
    }
}