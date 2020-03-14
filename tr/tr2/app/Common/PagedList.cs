using System.Collections.Generic;

namespace app.Common
{
    public class PagedList<T>
    {
        public PagedList(List<T> source, int totalPages)
        {
            Source = source;
            TotalPages = totalPages;
        }

        public List<T> Source { get; set; }
        public int TotalPages { get; set; }
    }
}
