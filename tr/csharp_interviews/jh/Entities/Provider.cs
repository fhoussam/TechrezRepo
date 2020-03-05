using System;
using System.Collections.Generic;
using System.Text;

namespace jh.Entities
{
    public class Provider
    {
        public int ProviderId { get; set; }
        public string ListUrl { get; set; }
        public string UrlPath { get; set; }
        public string DatePath { get; set; }
        public string PublisherPath { get; set; }
        public string TitlePath { get; set; }
        public string DescriptionPath { get; set; }
        public string EmptyPageIndicatorPath { get; set; }
        public DateParserType? DateExtractor { get; set; }
        public UrlTransformer? UrlTransformer { get; set; }
        public bool IsJobIdInQueryParam { get; set; }

        public List<UrlSpecialCharacter> UrlSpecialCharacter;

        public List<DescriptionUrlTransformer> DescriptionUrlTransformer;
    }
}
