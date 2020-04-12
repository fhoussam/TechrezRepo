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

            var propIndex = typeof(T).GetProperties().Count() > SortFieldIndex && SortFieldIndex >= 0 ? SortFieldIndex : 0;
            string propertyName = typeof(T).GetProperties()[propIndex].Name;

            var rawDataTsk = mainQuery
                .OrderBy($"{propertyName} {(IsDesc ? "desc" : string.Empty)}")
                .Skip(PageIndex * PagerParams.PageSize)
                .Take(PagerParams.PageSize)
                .ToListAsync()
                ;

            await Task.WhenAll(totalRowsTsk, rawDataTsk);

            int totalPages = (int)Math.Ceiling(totalRowsTsk.Result / (double)PagerParams.PageSize);

            return new PagedList<T>(rawDataTsk.Result, totalPages);
        }
    }
}
