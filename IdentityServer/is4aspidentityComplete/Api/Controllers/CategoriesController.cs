using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class Category
    {
        public int id { get; set; }
        public string description { get; set; }

        public static List<Category> Categories = new List<Category>()
        {
            new Category(){ id = 1, description = "Graphics card" },
            new Category(){ id = 2, description = "Power supply" },
            new Category(){ id = 3, description = "CPU" },
        }; 
    }
    
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            return Category.Categories;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
