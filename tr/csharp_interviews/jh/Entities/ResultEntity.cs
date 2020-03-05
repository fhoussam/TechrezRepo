using System;
using System.Collections.Generic;
using System.Linq;

namespace jh.Entities
{
    public class ResultEntity
    {
        public int ResultEntityId { get; set; }
        public string Path { get; set; }
        public DateTime PublishDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string RefUrl { get; set; }
        public Provider Provider { get; set; }
        public int ProviderId { get; set; }
        public SearchJob SearchJob { get; set; }
        public int SearchJobId { get; set; }
    }
    public class SearchResultEntity
    {
        public int ResultEntityId { get; set; }
        public DateTime PublishDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string RefUrl { get; set; }
        public Provider Provider { get; set; }
    }

    public static class EntityExtensions 
    {
        public static List<string> GetRequiredMatches(this SearchResultEntity resultEntity, List<Keyword> keywords) 
        {
            return keywords.Where(x => resultEntity.Description.ToLower().Contains(x.Value.ToLower())).Select(x => x.Value).ToList();
        }
    }

    public class CachedUrl
    {
        public int CachedUrlId { get; set; }
        public string Path { get; set; }
        public Provider Provider { get; set; }
        public int ProviderId { get; set; }
        public SearchJob SearchJob { get; set; }
        public int SearchJobId { get; set; }
    }
}