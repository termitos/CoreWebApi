using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CoreWebApi.Configuration
{
    /// <summary>
    /// Swagger configuration
    /// </summary>
    public static class Swagger
    {
        /// <summary>
        /// Configures the swagger.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("V1", GetApiInfo());
                    options.IncludeXmlComments(GetXmlCommentsFilePath());
                    options.DescribeAllEnumsAsStrings();
                    options.DescribeStringEnumsInCamelCase();
                    options.IgnoreObsoleteActions();
                    options.IgnoreObsoleteProperties();                    
                });

            return services;
        }

        /// <summary>
        ///     Use the swagger.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c => { c.RouteTemplate = "api-docs/{documentName}/swagger.json"; });

            app.UseSwaggerUI(
                options =>
                {
                    options.DocExpansion(DocExpansion.None);
                    options.RoutePrefix = "api-docs";
                    options.DocumentTitle = "ASP.NET Core 2.2 WebAPI";
                    options.SwaggerEndpoint("V1/swagger.json", "V1");
                });

            return app;
        }

        /// <summary>
        ///     Gets the XML comments file path.
        /// </summary>
        /// <returns></returns>
        /// <value>
        ///     The XML comments file path.
        /// </value>
        private static string GetXmlCommentsFilePath()
        {
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
            return Path.Combine(basePath, fileName);
        }

        
        private static Info GetApiInfo()
        {
            var info = new Info
            {
                Title = "ASP.NET Core 2.2 WebAPI",
                Version = "V1",
                Description = "ASP.NET Core 2.2 WebAPI."
            };
            return info;
        }
    }
}