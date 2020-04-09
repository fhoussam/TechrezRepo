using System.Collections.Generic;

namespace app.Common
{
    public class PagedList<T>
    {
        public PagedList(IEnumerable<T> source, int totalPages)
        {
            Source = source;
            TotalPages = totalPages;
        }

        public IEnumerable<T> Source { get; set; }
        public int TotalPages { get; set; }
    }
}
