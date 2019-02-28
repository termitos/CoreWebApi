using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreWebApi.Configuration
{
    /// <summary>
    /// Errors handler
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        /// <summary>
        ///     The next
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        ///     The environment
        /// </summary>
        private readonly IHostingEnvironment _environment;

        /// <summary>
        ///     The logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ErrorHandlingMiddleware" /> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="env">The env.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public ErrorHandlingMiddleware(
            RequestDelegate next,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _environment = env;
            _logger = loggerFactory.CreateLogger<ErrorHandlingMiddleware>();
        }

        /// <summary>
        ///     Task to catch exceptions.
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex).ConfigureAwait(false);
            }
        }

        /// <summary>
        ///     Handles the exception asynchronously.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogCritical(exception, "Unhandled exception occurred.");

            var errorViewModel = _environment.IsDevelopment() ? new { Errors = $"Unhandled exception occurred: {exception.Message}; InnerException: {exception.InnerException?.Message}; StackTrace: {exception.StackTrace}. Please contact the system administrator." } : new { Errors = $"Unhandled exception occurred: {exception.Message}. Please contact the system administrator." };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorViewModel));
        }
    }
}