using Dal.BusinessExceptions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Dal.Extentions
{
    public static class IQueryableExtention
    {
        public static async Task<T> SingleOrDefaultAsync404<T>(this IQueryable<T> iqueryable, Expression<Func<T, bool>> expression)
        {
            var data = await iqueryable.SingleOrDefaultAsync(expression);
            if (data == null) throw new NotFoundException();
            return data;
        }
    }
}
