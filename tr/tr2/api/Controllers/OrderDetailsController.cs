using System.Threading.Tasks;
using app.Operations.ProductOrders.Commands.DeleteOrderDetail;
using app.Operations.ProductOrders.Commands.EditOrderDetail;
using app.Operations.ProductOrders.Queries.GetOrderDetails;
using app.Operations.ProductOrders.Queries.IsQuantityValidQuery;
using app.Operations.ProductOrders.Queries.SearchOrderDetails;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : AppBaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchOrderDetailsQuery searchOrderDetailsQuery)
        {
            return Ok(await Mediator.Send(searchOrderDetailsQuery));
        }

        [HttpGet]
        [Route("{productID:int}/{orderID:int}")]
        public async Task<IActionResult> Find([FromRoute] int productID, [FromRoute] int orderID, [FromQuery] bool isEdit) 
        {
            return Ok(await Mediator.Send(new GetOrderDetailQuery(productID, orderID, isEdit)));
        }

        [HttpGet]
        [Route("isquantityvalid")]
        public async Task<IActionResult> IsQuantityValid(IsQuantityValidQuery isQuantityValidQuery) 
        {
            return Ok(await Mediator.Send(isQuantityValidQuery));
        }

        [HttpPost]
        public async Task<IActionResult> Post(EditOrderDetailCommand editOrderDetailCommand) 
        {
            return Ok(await Mediator.Send(editOrderDetailCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteOrderDetailCommand deleteOrderDetailCommand) 
        {
            return Ok(await Mediator.Send(deleteOrderDetailCommand));
        }
    }
}