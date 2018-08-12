using System.Linq;
using System.Threading.Tasks;
using Api.CustomFilters;
using Api.Dto;
using Api.Dto.Post;
using Api.Dto.Put;
using Dal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IDalService _dal;
        private readonly IMemoryCache _cache;
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ProductsController));

        public ProductsController(IDalService dal, IMemoryCache cache)
        {
            _dal = dal;
            _cache = cache;
        }

        [HttpGet]
        //only produces xml
        [Produces("application/xml", "application/json")]
        public async Task<IActionResult> Get()
        {
            var model = await _dal.GetAllProductsAsync();
            var data = model.Select(x => x.ToDtoGet()).ToList();
            return Ok(data);
        }

        [HttpGet]
        //if the route does not respect the route filter, it simply returns a 404 without hitting the action
        //here, the conditions are : int, between 1 and 1000
        [Route("{id:int:regex(^([[1-9]][[0-9]]{{0,2}}|1000)$)}")]
        //produces both xml and json, priority for xml when Accept is empty
        [Produces("application/xml", "application/json")]
        public async Task<IActionResult> Get(int id)
        {
            //log.DebugFormat("Getting item {0}", id);
            var model = await _dal.GetProductByIdAsync(id);
            var data = model.ToDtoGet();
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductDtoPut product)
        {
            var model = product.ToModel();
            var updatedObject = await _dal.UpdateProductAsync(model);
            return Created($"{ Request.Path}", updatedObject.ToDtoGet());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _dal.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpPost]
        [Consumes("application/xml", "application/json")]
        public async Task<IActionResult> Post([FromBody] ProductDtoPost product)
        {
            var model = product.ToModel();
            int productId = await _dal.AddProductAsync(model);
            return Created($"{ Request.Path}/{productId}", null);
        }
    }
}