using System.Threading.Tasks;
using app.Operations.Product.Commands.DeleteProduct;
using app.Operations.Product.Commands.EditProduct;
using app.Operations.Product.Queries.GetProductDetails;
using app.Operations.Product.Queries.IsProductNameUnique;
using app.Operations.Product.Queries.SearchProduct;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : AppBaseController
    {
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteProductCommand deleteProductCommand)
        {
            return Ok(await Mediator.Send(deleteProductCommand));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EditProductCommand editProductCommand) 
        {
            return Ok(await Mediator.Send(editProductCommand));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchProductQuery searchProductQuery) 
        {
            return Ok(await Mediator.Send(searchProductQuery));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetProductDetailsQuery() { ProductId = id }));
        }

        [HttpGet]
        [Route("IsExistingProductName")]
        public async Task<IActionResult> IsExistingProductName([FromQuery] string productName)
        {
            return Ok(await Mediator.Send(new IsProductNameUniqueQuery() { ProductName = productName }));
        }
    }
}