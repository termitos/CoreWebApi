using System.Reflection;
using CoreWebApi.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Steeltoe.Management.Endpoint.Health;
using Steeltoe.Management.Endpoint.Info;
using Steeltoe.Management.Endpoint.Mappings;
using Steeltoe.Management.Endpoint.Metrics;
using Steeltoe.Management.Endpoint.Refresh;
using Steeltoe.Management.Endpoint.ThreadDump;
using Steeltoe.Management.Endpoint.Trace;

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


        /// <summary>
        /// Configures actuators.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureActuators(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHealthActuator(configuration);
            services.AddInfoActuator(configuration);
            services.AddRefreshActuator(configuration);
            services.AddMetricsActuator(configuration);
            services.AddMappingsActuator(configuration);
            services.AddTraceActuator(configuration);
            services.AddThreadDumpActuator(configuration);

            return services;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ApiInfoContributor : IInfoContributor
    {
        /// <summary>
        /// Contributes the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Contribute(IInfoBuilder builder)
        {
            var assembly = Assembly.GetEntryAssembly();
            var assemblyVersion = assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                .InformationalVersion;
            var assemblyBuildDate = System.IO.File.GetLastWriteTime(assembly?.Location);
            builder.WithInfo("info", new OpenApiInfo
            {
                Title = "ASP.NET Core 2.2 WebAPI",
                Version = $"V1, {assemblyVersion} [{assemblyBuildDate}]",
                Description = "ASP.NET Core 2.2 WebAPI."
            });
        }
    }
}