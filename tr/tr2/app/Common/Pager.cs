using System;
using System.Collections.Generic;
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
        public async Task<PagedList<T>> CreatePagedList<T>(IQueryable<T> mainQuery)
        {
            var totalCount = await mainQuery.CountAsync();

            if (totalCount == 0)
                return new PagedList<T>(new List<T>().AsQueryable(), 0);

            else 
            {
                var propIndex = typeof(T).GetProperties().Count() > SortFieldIndex && SortFieldIndex >= 0 ? SortFieldIndex : 0;
                string propertyName = typeof(T).GetProperties()[propIndex].Name;
                var list = await mainQuery
                    .OrderBy($"{propertyName} {(IsDesc ? "desc" : string.Empty)}")
                    .Skip(PageIndex * PagerParams.PageSize)
                    .Take(PagerParams.PageSize)
                    .ToListAsync();
                int totalPages = (int)Math.Ceiling(totalCount / (double)PagerParams.PageSize);
                return new PagedList<T>(list, totalPages);
            }
        }
    }
}
