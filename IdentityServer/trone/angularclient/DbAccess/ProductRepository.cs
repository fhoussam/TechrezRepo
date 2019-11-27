using angularclient.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace angularclient.DbAccess
{
    public class ProductRepository : TechRezBaseRepository<Product, TechRezDbContext>
    {
        private TechRezDbContext _context;
        public ProductRepository(TechRezDbContext context) : base(context)
        {
            this._context = context;
        }

        // We can add new methods specific to the product repository here in the future
        public async Task<List<Category>> GetCategories() {
            return await _context.Set<Category>().Take(10).ToListAsync();
        }
    }
}