using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApi.Controllers
{
    /// <summary>
    /// Values controller
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ValuesController : Controller
    {
        /// <summary>
        /// Get values
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[] { "value1", "value2" };
        }

        /// <summary>
        ///  Get value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Store value
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// Upgrade value
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// Delete value
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
