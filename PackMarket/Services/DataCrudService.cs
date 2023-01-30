using Blazorise.Extensions;

using Microsoft.EntityFrameworkCore;

using PackMarket.Data;
using PackMarket.Data.Models;
using PackMarket.Data.ViewModels;
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
        public async Task<List<Category>> GetCategoriesAndProductsAsync()
        {
            return await dbContext.Categories.Include(c => c.Products).ToListAsync();
        }
        //Categories
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }
        public async Task<Category> GetCategory(int id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Category> GetCategory(string url)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(c => c.Url==url);
        }
        public async Task SaveCategoryAsync(Category category)
        {
            
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
        }
        public async Task UpdateCategoryAsync(Category category)
        {
           
            dbContext.Categories.Update(category);
            await dbContext.SaveChangesAsync();
        }
        public async Task DeleteCategoryAsync(int id)
        {
            var category = await dbContext.Categories.FindAsync(id);
            if(category != null)
            {
                dbContext.Categories.Remove(category);
                await dbContext.SaveChangesAsync();
            }
        }
        //Products
        internal async Task<List<Product>> GetProductsAsync()
        {
            return await dbContext.Products.ToListAsync();
        }
        internal async Task<List<Product>> GetProductsOrderedByUrlAsync()
        {
            return await dbContext.Products.OrderBy(p=>p.Url).ToListAsync();
        }
        internal async Task SaveProductAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
        }

        internal async Task UpdateProductAsync(Product product)
        {
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync();
        }

        internal async Task DeleteProductAsync(int id)
        {
            var prod= await dbContext.Products.FindAsync(id);
            dbContext.Remove(prod);
            await dbContext.SaveChangesAsync();
        }

        internal async Task<List<Tag>> GetTags()
        {
            return await dbContext.Tags.ToListAsync(); 
        }
    }
}
