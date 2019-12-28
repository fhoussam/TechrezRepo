using angularclient.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace angularclient.DbAccess
{
    public class FeedRepository : TechRezBaseRepository<Feed, TechRezDbContext>
    {
        private TechRezDbContext _context;
        public FeedRepository(TechRezDbContext context) : base(context)
        {
            this._context = context;
        }

        public override async Task<List<Feed>> GetAll()
        {
            return await _context.Set<Feed>().OrderByDescending(x=> x.DateTimeStamp).Take(10).ToListAsync();
        }
    }
}