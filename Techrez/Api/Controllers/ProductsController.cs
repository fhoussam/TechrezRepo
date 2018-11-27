using System.Threading.Tasks;
using Api.DataServices;
using Api.Dto;
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
        private readonly IProductService _productService;
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ProductsController));

        public ProductsController(IDalService dal, IMemoryCache cache, IProductService productService)
        {
            _dal = dal;
            _cache = cache;
            _productService = productService;
        }

        [HttpGet]
        //only produces xml
        [Produces("application/xml", "application/json")]
        public async Task<IActionResult> Get()
        {
            var dto = await _productService.GetProductAsync();
            return Ok(dto);
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
            var dto = await _productService.GetProductByIdAsync(id);
            return Ok(dto);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductDtoPut product)
        {
            var dto = await _productService.UpdateProductAsync(product);
            return Created($"{ Request.Path}", dto);
        }

        [HttpDelete]
        //[Route("{id:int}")] //for delete method, we want to avoid passing id in the url
        public async Task<IActionResult> Delete([FromBody] string id)
        {
            await _productService.DeleteProductAsync(int.Parse(id.ToString()));
            return NoContent();
        }

        [HttpPost]
        [Consumes("application/xml", "application/json")]
        public async Task<IActionResult> Post([FromBody] ProductDtoPost product)
        {
            var dto = await _productService.AddProductAsync(product);
            return Created($"{ Request.Path}/{dto}", null);
        }

        [HttpGet]
        [Route("init")]
        public IActionResult InitData()
        {
            _productService.InitData();
            return NoContent();
        }
    }
}