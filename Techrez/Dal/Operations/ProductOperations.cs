using Dal.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Dal.Extentions;

namespace Dal
{
    partial class DalService
    {
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await DbContext.Products.Include(x => x.Category).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await DbContext.Products.Include(x => x.Category)
                .SingleOrDefaultAsync404(x => x.Id == id);
        }
        
        public async Task<Product> UpdateProductAsync(Product product)
        {
            var toupdate = await DbContext.Products.Include(x => x.Category).SingleOrDefaultAsync404(x => x.Id == product.Id);

            toupdate.Description = product.Description;
            toupdate.CategoryID = product.CategoryID;
            toupdate.Price = product.Price;
            toupdate.Stock = product.Stock;

            await DbContext.SaveChangesAsync();
            return toupdate;
        }

        public async Task<int> AddProductAsync(Product product)
        {
            await DbContext.Products.AddAsync(product);
            await DbContext.SaveChangesAsync(); //promise (savecganges) should return 1 if success -> to manage later
            return product.Id;
        }

        public async Task DeleteProductAsync(int id)
        {
            var todelete = await DbContext.Products.Include(x => x.Category).SingleOrDefaultAsync404(x => x.Id == id);

            DbContext.Products.Remove(todelete);
            await DbContext.SaveChangesAsync();
        }

        public void InitData()
        {
            DbContext.Products.RemoveRange(DbContext.Products);
            DbContext.Categories.RemoveRange(DbContext.Categories);
            DbContext.SaveChanges();
            DbContext.Database.ExecuteSqlCommand(new RawSqlString("delete from __EFMigrationsHistory where MigrationId like '%data%'"));
            DbContext.Database.Migrate();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await DbContext.Categories.ToListAsync();
        }
    }
}
