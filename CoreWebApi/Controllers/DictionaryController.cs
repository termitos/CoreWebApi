using System.Collections.Generic;
using System.Threading.Tasks;
using CoreWebApi.Model;
using CoreWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApi.Controllers
{
    /// <summary>
    /// Values controller
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DictionaryController : Controller
    {
        /// <summary>
        /// The service
        /// </summary>
        private readonly DictionaryService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public DictionaryController(DictionaryService service)
        {
            this._service = service;
        }
       
        /// <summary>
        /// Get items
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Item>>> Get([FromQuery]FilterRequest filter)
        {
            List<Item> items;
            if (string.IsNullOrEmpty(filter.Attribute) && string.IsNullOrEmpty(filter.Value))
            {
                items = await _service.GetItems();
            }
            else
            {
                items = await _service.GetItemsWithFilter(filter);
            }
            return new JsonResult(new {items});
        }

        /// <summary>
        /// Save item
        /// </summary>
        /// <param name="name"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Post([FromQuery] string name, [FromQuery] string key, [FromQuery] string value, [FromQuery] string description)
        {
            var itemId = await _service.SaveItem(name, key, value, description);
            return StatusCode(StatusCodes.Status201Created, itemId);
        }

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Put(string id, [FromQuery] string name, [FromQuery] string key, [FromQuery] string value, [FromQuery] string description)
        {
            await _service.UpdateItem(id, name, key, value, description);
            return Ok();
        }


    }
}
