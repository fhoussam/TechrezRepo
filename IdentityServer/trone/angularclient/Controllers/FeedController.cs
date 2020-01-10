using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using angularclient.DbAccess;
using angularclient.Models;
using angularclient.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace angularclient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ShouldBeAuthorized")]

    public class FeedController : ControllerBase
    {
        private FeedRepository _feedRepository;
        private IWebHostEnvironment _webHostEnvironment;
        private IHubContext<FeedHub> _hub;

        public FeedController(
            FeedRepository repository, 
            IWebHostEnvironment webHostEnvironment,
            IHubContext<FeedHub> hub
        ) 
        {
            _feedRepository = repository;
            _webHostEnvironment = webHostEnvironment;
            _hub = hub;
        }

        [Route("triggerFakeOperation")]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public IActionResult TriggerFakeOperation()
        {
            //var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("transferchartdata", DataManager.GetData()));
            _hub.Clients.All.SendAsync("transferfeeddata", DataManager.GetData());
            return Ok(new { Message = "Request Completed" });
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

    public static class DataManager
    {
        public static List<Feed> GetData()
        {
            return new List<Feed>(){ 
                new Feed()
                {
                    Code = new Guid().ToString(),
                    DateTimeStamp = DateTime.Now,
                    OperationType = "SAVE_PRODUCT",
                    UserName = "User N-" + new Random().Next(1, 20)
                } 
            };
        }
    }

    //public class TimerManager
    //{
    //    private Timer _timer;
    //    private AutoResetEvent _autoResetEvent;
    //    private Action _action;

    //    public DateTime TimerStarted { get; }

    //    public TimerManager(Action action)
    //    {
    //        _action = action;
    //        _autoResetEvent = new AutoResetEvent(false);
    //        _timer = new Timer(Execute, _autoResetEvent, 1000, 2000);
    //        TimerStarted = DateTime.Now;
    //    }

    //    public void Execute(object stateInfo)
    //    {
    //        _action();

    //        if ((DateTime.Now - TimerStarted).Seconds > 60)
    //        {
    //            _timer.Dispose();
    //        }
    //    }
    //}
}