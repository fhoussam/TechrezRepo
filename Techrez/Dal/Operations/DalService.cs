using Common;
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
        Task<ProductSearchResult> GetProductsAsync(SearchSetting searchSetting);
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> UpdateProductAsync(Product product);
        Task<int> AddProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        void InitData();
    }

    public class ProductSearchResult
    {
        public int Count { get; set; }
        public List<Product> PageData { get; set; }
    }
}
