using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto
{
    [Serializable]
    public class ProductSearchSetting : SearchSetting
    {
        [SearchFilterAttribut(Comparator.like)]
        [StringLength(maximumLength: 30, MinimumLength = 1)]
        public string Description { get; set; }

        [SearchFilterAttribut(Comparator.lt)]
        [Range(0, double.MaxValue)]
        public int? MinStock { get; set; }

        [SearchFilterAttribut(Comparator.gt)]
        [Range(0, double.MaxValue)]
        public int? MaxStock { get; set; }

        [SearchFilterAttribut(Comparator.lte)]
        [Range(0, double.MaxValue)]
        public float? MinPrice { get; set; }

        [SearchFilterAttribut(Comparator.gte)]
        [Range(0, double.MaxValue)]
        public float? MaxPrice { get; set; }

        [SearchFilterAttribut(Comparator.eq)]
        public int? CategoryID { get; set; }
    }
}
