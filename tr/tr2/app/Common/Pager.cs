using System.Linq;

namespace app.Common
{
    public class Pager
    {
        public string SortField { get; set; }
        public bool IsDesc { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public string GetValidatedSortField()
        {
            var props = GetType().GetProperties();

            if (!string.IsNullOrEmpty(SortField))
            {
                var prop = props.FirstOrDefault(x => string.Equals(SortField, x.Name, System.StringComparison.OrdinalIgnoreCase));
                if (prop != null)
                    return prop.Name;
            }

            return props.First().Name;
        }
    }
}
