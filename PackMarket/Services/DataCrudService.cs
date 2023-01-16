using Blazorise.Extensions;

using Microsoft.EntityFrameworkCore;

using PackMarket.Data;
using PackMarket.Data.Models;
using PackMarket.Extentions;

namespace PackMarket.Services
{
    public class DataCrudService
    {
        private readonly ApplicationDbContext dbContext;
        public DataCrudService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Category>> GetCategories()
        {
            return await dbContext.Categories.ToListAsync();
        }
        public async Task SaveCategory(Category category)
        {
            
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
        }
        public async Task UpdateCategory(Category category)
        {
           
            dbContext.Categories.Update(category);
            await dbContext.SaveChangesAsync();
        }
        public async Task DeleteCategory(int id)
        {
            var category = await dbContext.Categories.FindAsync(id);
            if(category != null)
            {
                dbContext.Categories.Remove(category);
                await dbContext.SaveChangesAsync();
            }
        }

        internal async Task<List<Product>> GetProducts()
        {
            return await dbContext.Products.ToListAsync();
        }

        internal async Task Saveproduct(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
        }

        internal async Task UpdateProduct(Product product)
        {
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync();
        }

        internal async Task DeleteProduct(int id)
        {
            var prod= await dbContext.Products.FindAsync(id);
            dbContext.Remove(prod);
            await dbContext.SaveChangesAsync();
        }
    }
}
