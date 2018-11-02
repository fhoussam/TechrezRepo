using Dal;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dto;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        public IDalService dalService;

        public CategoriesController(IDalService dalService)
        {
            this.dalService = dalService;
        }

        public async Task<IActionResult> Get()
        {
            var model = await dalService.GetCategoriesAsync();
            var dtoCategories = model.Select(x => x.ToDtoGet());
            return Ok(dtoCategories);
        }
    }
}
