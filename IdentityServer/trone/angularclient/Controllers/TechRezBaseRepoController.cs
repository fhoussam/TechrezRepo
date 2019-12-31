using System.Collections.Generic;
using System.Threading.Tasks;
using angularclient.DbAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace angularclient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class TechRezBaseRepoController<TEntity, TRepository> : ControllerBase
        where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {
        private readonly TRepository repository;
        protected IWebHostEnvironment _webHostEnvironment;

        public TechRezBaseRepoController(TRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            this.repository = repository;
            this._webHostEnvironment = webHostEnvironment;
        }

        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await repository.GetAll();
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(string id)
        {
            var movie = await repository.Get(id);
            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, TEntity movie)
        {
            if (id != movie.Code)
            {
                return BadRequest();
            }
            await repository.Update(movie);
            return NoContent();
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity movie)
        {
            await repository.Add(movie);
            return CreatedAtAction("Get", new { id = movie.Code }, movie);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(string id)
        {
            var movie = await repository.Delete(id);
            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }

    }
}