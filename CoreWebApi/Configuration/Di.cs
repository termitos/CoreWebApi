using System;
using Amazon.DynamoDBv2;
using CoreWebApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Common.HealthChecks;
using Steeltoe.Management.Endpoint.Info;

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
            services.AddSingleton<IHealthContributor, ApiHealthContributor>();
            services.AddSingleton<IInfoContributor, ApiInfoContributor>();

            var dynamoDbConfig = configuration.GetSection("DynamoDb");
            var runLocalDynamoDb = dynamoDbConfig.GetValue<bool>("LocalMode");
            if (runLocalDynamoDb)
            {
                services.AddSingleton<IAmazonDynamoDB>(sp =>
                {
                    var clientConfig = new AmazonDynamoDBConfig { ServiceURL = dynamoDbConfig.GetValue<string>("LocalServiceUrl") };
                    return new AmazonDynamoDBClient(clientConfig);
                });
            }
            else
            {
                services.AddDefaultAWSOptions(configuration.GetAWSOptions());
                services.AddAWSService<IAmazonDynamoDB>();
            }
            
            services.AddTransient<DictionaryService>();

            return services.BuildServiceProvider();
        }
    }
}