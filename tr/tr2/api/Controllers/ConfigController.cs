using System.Collections.Generic;
using System.Threading.Tasks;
using app.Common.Enums;
using app.Operations.Config.Commands;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : AppBaseController
    {
        [HttpGet]
        [Route("dropdownlists")]
        public async Task<IActionResult> DropdownLists(IEnumerable<DropDownListIdentifier> requestedDropDownLists)
        {
            return Ok(await Mediator.Send(new GetDropDownListsQuery() { requestedDropDownListData = requestedDropDownLists }));
        }
    }
}