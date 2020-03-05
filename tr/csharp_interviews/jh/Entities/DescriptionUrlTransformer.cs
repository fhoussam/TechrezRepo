using System;
using System.Collections.Generic;
using System.Text;

namespace jh.Entities
{
    public class DescriptionUrlTransformer
    {
        public int DescriptionUrlTransformerId { get; set; }
        public string Value { get; set; }
        public string Replacer { get; set; }
        public Provider Provider { get; set; }
        public int ProviderId { get; set; }
    }
}
