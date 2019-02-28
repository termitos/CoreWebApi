using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CoreWebApi.Filters
{
    /// <summary>
    /// Global action filter which executes before and after requested MVC action.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IActionFilter" />
    public class GlobalActionFilter : IActionFilter
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalActionFilter"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public GlobalActionFilter(ILogger<GlobalActionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Called before the action executes, after model binding is complete.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {

                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug(
                        $"Action [{context.HttpContext.Request.Method}] [{context.HttpContext.Request.Path}] requested."
                    );
                }
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Called after the action executes, before the action result.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext" />.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                {

                    _logger.LogDebug(
                        $"Action [{context.HttpContext.Request.Method}] [{context.HttpContext.Request.Path}] executed.");
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}