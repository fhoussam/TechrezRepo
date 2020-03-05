using System;
using System.Collections.Generic;
using System.Text;

namespace jh.Entities
{
    public class SearchJob
    {
        public int SearchJobId { get; set; }
        public DateTime SearchDate { get; set; }
        public double DurationInMs { get; set; }
        public List<ResultEntity> ResultEntity { get; set; }
    }
}