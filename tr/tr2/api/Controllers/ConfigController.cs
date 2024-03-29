﻿using System.Threading.Tasks;
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
        [Route("categories")]
        public async Task<IActionResult> GetCategories() 
        {
            return Ok(await Mediator.Send(new GetDropDownListsQuery(new DropDownListIdentifier[] { DropDownListIdentifier.Categories })));
        }
    }
}