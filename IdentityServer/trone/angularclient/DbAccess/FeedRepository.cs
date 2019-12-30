using angularclient.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace angularclient.DbAccess
{
    public class FeedRepository 
    {
        private TechRezDbContext _context;
        public FeedRepository(TechRezDbContext context) 
        {
            this._context = context;
        }

        //query to be optimized
        public async Task<List<Feed>> GetAll(string lastSeenCode = null)
        {
            DateTime? lastSeenDateTimeStamp = null;

            if (!string.IsNullOrEmpty(lastSeenCode)) 
                lastSeenDateTimeStamp = _context.Set<Feed>().Single(ls => ls.Code == lastSeenCode).DateTimeStamp;

            return await (
                from f in _context.Set<Feed>()
                where (string.IsNullOrEmpty(lastSeenCode)) || (f.DateTimeStamp <= lastSeenDateTimeStamp
                    && f.Code != lastSeenCode) 
                select f
            )
            .OrderByDescending(x => x.DateTimeStamp)
            .Take(20)
            .ToListAsync();
        }

        public async Task<Feed> Add(Feed entity)
        {
            entity.Code = Guid.NewGuid().ToString();
            _context.Set<Feed>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}