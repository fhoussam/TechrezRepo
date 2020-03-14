using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Operations.Category.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : AppBaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            return Ok(await Mediator.Send(new GetAllCategoriesQuery()));
        }
    }
}