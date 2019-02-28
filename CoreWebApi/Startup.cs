using CoreWebApi.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoreWebApi
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="currentEnvironment">The current environment.</param>
        public Startup(IConfiguration configuration, IHostingEnvironment currentEnvironment)
        {
            Configuration = configuration;
            CurrentEnvironment = currentEnvironment;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <value>
        /// The environment.
        /// </value>
        public IHostingEnvironment CurrentEnvironment { get; }

        /// <summary>
        /// Configure services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureMvc();
            services.ConfigureCors();
            services.ConfigureSwagger(Configuration);
            services.ConfigureDependencyInjection(Configuration);
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            if (CurrentEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttps(CurrentEnvironment);
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseSerilog(CurrentEnvironment, Configuration, loggerFactory);
            app.UseCors(Api.DefaultCorsPolicyName);
            app.UseAuthentication();
            app.UseSwagger();
            app.UseMvc();
        }
    }
}
