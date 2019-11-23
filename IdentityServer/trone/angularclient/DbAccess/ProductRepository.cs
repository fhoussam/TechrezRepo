using angularclient.DbAccess;
using angularclient.Models;

namespace angularclient.DbAccess
{
    public class ProductRepository : TechRezBaseRepository<Product, TechRezDbContext>
    {
        public ProductRepository(TechRezDbContext context) : base(context)
        {

        }
        // We can add new methods specific to the product repository here in the future
    }
}