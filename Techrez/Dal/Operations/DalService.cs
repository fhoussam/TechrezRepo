using Dal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dal
{
    public partial class DalService : IDalService
    {
        private TechrezDbContext DbContext;

        public DalService(TechrezDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }

    public interface IDalService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> UpdateProductAsync(Product product);
        Task<int> AddProductAsync(Product product);
        Task DeleteProductAsync(int id);
        void InitData();
    }
}
