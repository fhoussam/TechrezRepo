using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace app.Common
{
    public class Pager
    {
        public int SortFieldIndex { get; set; }
        public bool IsDesc { get; set; }
        public int PageIndex { get; set; }
        public async Task<PagedList<T>> CreatePagedList<T, R>(IQueryable<T> mainQuery, IQueryable<R> countQuery)
        {
            var totalRowsTsk = countQuery.CountAsync();

            mainQuery = mainQuery.OrderBy(SortFieldIndex + (IsDesc ? " desc" : string.Empty));
           
            var rawDataTsk = mainQuery
                .Skip(PageIndex * PagerParams.PageSize)
                .Take(PagerParams.PageSize).ToListAsync();

            await Task.WhenAll(totalRowsTsk, rawDataTsk);

            int totalPages = (int)Math.Ceiling(totalRowsTsk.Result / (double)PagerParams.PageSize);

            return new PagedList<T>(rawDataTsk.Result, totalPages);
        }
    }
}
