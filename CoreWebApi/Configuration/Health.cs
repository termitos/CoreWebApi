using Microsoft.Extensions.Configuration;
using Steeltoe.Common.HealthChecks;

namespace CoreWebApi.Configuration
{
    /// <summary>
    /// Class ApiHealthContributor.
    /// </summary>
    /// <seealso cref="Steeltoe.Common.HealthChecks.IHealthContributor" />
    public class ApiHealthContributor : IHealthContributor
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiHealthContributor"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public ApiHealthContributor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Check the health of a resource
        /// </summary>
        /// <returns>The result of checking the health of a resource</returns>
        public HealthCheckResult Health()
        {
            var result = new HealthCheckResult {Status = HealthStatus.UP};
            result.Details.Add("status", result.Status.ToString());
            result.Description = "Health Check Result.";

            return result;
        }

        /// <summary>
        /// Gets an identifier for the type of check being performed
        /// </summary>
        /// <value>The identifier.</value>
        public string Id => "CoreWebApiHealthContributor";
    }
}
