using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace angularclient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [Route("nonsecure")]
        public string NonSecure()
        {
            return "Not secure";
        }

        [Authorize]
        [Route("secure")]
        public string Secure()
        {
            return "Secure";
        }
    }
}