using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using angularclient.DbAccess;
using angularclient.Models;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;

namespace angularclient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        private FeedRepository _feedRepository;
        private HostingEnvironment _hostingEnvironment;

        public FeedController(FeedRepository repository, HostingEnvironment hostingEnvironment) 
        {
            this._feedRepository = repository;
            this._hostingEnvironment = hostingEnvironment;
        }

        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feed>>> Get(string lastSeen = null)
        {
            return await _feedRepository.GetAll(lastSeen);
        }

        [HttpPost]
        public async Task<ActionResult<Feed>> Post(Feed feed)
        {
            await _feedRepository.Add(feed);
            return CreatedAtAction("Get", new { id = feed.Code }, feed);
        }
    }
}