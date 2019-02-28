using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
namespace CoreWebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public static class Logging
    {
        /// <summary>
        /// Use the Serilog framework.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSerilog(
            this IApplicationBuilder app,
            IHostingEnvironment environment,
            IConfiguration configuration,
            ILoggerFactory loggerFactory)
        {
            var logConfig = new LoggerConfiguration().ReadFrom.Configuration(configuration);          
            loggerFactory.AddSerilog(logConfig.CreateLogger());        
            return app;
        }
    }  
}