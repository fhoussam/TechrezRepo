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
    public class FeedController : TechRezBaseRepoController<Feed, FeedRepository>
    {
        private FeedRepository _feedRepository;
        private HostingEnvironment _hostingEnvironment;

        public FeedController(FeedRepository repository, HostingEnvironment hostingEnvironment) : base(repository, hostingEnvironment)
        {
            this._feedRepository = repository;
            this._hostingEnvironment = hostingEnvironment;
        }
    }
}