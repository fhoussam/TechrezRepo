using System.Threading.Tasks;
using app.Operations.Product.Commands.DeleteProduct;
using app.Operations.Product.Commands.EditProduct;
using app.Operations.Product.Queries.SearchProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteProductCommand deleteProductCommand)
        {
            return Ok(await _mediator.Send(deleteProductCommand));
        }

        [HttpPost]
        public async Task<IActionResult> Post(EditProductCommand editProductCommand) 
        {
            return Ok(await _mediator.Send(editProductCommand));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchProductQuery searchProductQuery) 
        {
            return Ok(await _mediator.Send(searchProductQuery));
        }
    }
}