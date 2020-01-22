using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApi.Controllers
{
	/// <summary>
    /// Version controller
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class VersionController : Controller
    {
        /// <summary>
        /// Get version
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> Get()
        {
            var version = Environment.GetEnvironmentVariable("APP_VERSION");
            return version ?? "UNKNOWN";
        }
    }
}