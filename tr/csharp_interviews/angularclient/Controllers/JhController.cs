using System;
using System.Collections.Generic;
using System.Linq;
using jh;
using jh.Entities;
using jh.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace angularclient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JhController : ControllerBase
    {
        private readonly IHelper _helper;
        private readonly IJhDbContext _jhDbContext;

        public JhController(IHelper helper, IJhDbContext jhDbContext)
        {
            _helper = helper;
            _jhDbContext = jhDbContext;
        }

        [HttpGet]
        [Route("process")]
        public IEnumerable<ResultEntityDto> Process()
        {
            IHelper helper = new Helper();
            Search search = new Search(_helper, _jhDbContext);
            search.Start();
            return search.Result
                .Select(x => new ResultEntityDto(_jhDbContext.Keywords.ToList(), new ResultEntity() 
                {
                    Description = x.Value.Description,
                    Path = x.Key,
                    Provider = x.Value.Provider,
                    PublishDate = x.Value.PublishDate,
                    Publisher = x.Value.Publisher,
                    RefUrl = x.Value.RefUrl,
                    Title = x.Value.Title,
                }))
                .ToList().OrderByDescending(x => x.OtherMatches.Count);
        }

        [HttpGet]
        public IEnumerable<ResultEntityDto> Get([FromQuery]
            string sf, //sort field
            string m, //match
            int? d //days
            )
        {
            var keywords = _jhDbContext.Keywords.ToList();

            if (!keywords.Any(y => y.Value == m))
                m = string.Empty;

            var result = _jhDbContext.ResultEntities.Include(x => x.Provider)
                .Where(x => m == "" || x.Description.ToLower().Contains(m.ToLower()))
                .ToList()
                .Where(x => !d.HasValue || (DateTime.Now - x.PublishDate).TotalDays < d)
                .Select(x => new ResultEntityDto(keywords, x)).ToList();

            if (sf == "tmc")
                result = result.OrderByDescending(x => x.OtherMatches.Count + x.SpecialMatches.Count).ToList();

            else if (sf == "smc")
                result = result.OrderByDescending(x => x.SpecialMatches.Count).ToList();

            else
                result = result.OrderByDescending(x => x.PublishDate).ToList();

            return result;
        }
    }

    public class ResultEntityDto
    {
        public string Path { get; set; }
        public DateTime PublishDate { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Provider { get; set; }
        public List<string> OtherMatches { get; set; }
        public List<string> SpecialMatches { get; set; }
        public ResultEntityDto(List<Keyword> keywords, ResultEntity resultEntity)
        {
            Uri providerListUrl = new Uri(resultEntity.Provider.ListUrl);
            Path = $"http{(providerListUrl.Port == 443 ? "s" : string.Empty)}://{providerListUrl.Host}/{resultEntity.Path}";
            PublishDate = resultEntity.PublishDate;
            Title = resultEntity.Title;
            Publisher = resultEntity.Publisher;
            Provider = providerListUrl.Host;
            SpecialMatches = keywords.Where(x => !x.IsComplexe && !x.IsRequired && resultEntity.Description.ToLower().Contains(x.Value.ToLower())).Select(x => x.Value).ToList();
            OtherMatches = keywords.Where(x => !SpecialMatches.Any(y => y == x.Value) && resultEntity.Description.ToLower().Contains(x.Value.ToLower())).Select(x => x.Value).ToList();
        }
    }
}
