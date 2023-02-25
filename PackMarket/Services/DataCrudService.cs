using Blazorise.Extensions;

using Microsoft.EntityFrameworkCore;

using PackMarket.Data;
using PackMarket.Data.Enums;
using PackMarket.Data.Models;
using PackMarket.Data.ViewModels;
using PackMarket.Extentions;

using System.Xml.Linq;

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
        public List<Category> GetCategoriesAndProducts()
        {
            return dbContext.Categories.Include(c => c.Products).ToList();
        }
        //Categories
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Category> GetCategoryByUrlAsync(string url)
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
        internal async Task<Product> GetProductByIdAsync(int id)
        {
            return await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
        internal async Task<List<Product>> GetProductsTagsAsync()
        {
            return await dbContext.Products.Include(t=>t.Tags).ToListAsync();
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
        //TAGS
        internal async Task<List<Tag>> GetTagsAsync()
        {
            return await dbContext.Tags.ToListAsync(); 
        }
        internal async Task<Tag> GetTagByNameAsync(string name)
        {
            return await dbContext.Tags.FirstOrDefaultAsync(t=>t.Name==name);
        }
        internal async Task<Tag> GetTagByIdAsync(int id)
        {
            return await dbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
        }
        //CART
        internal async Task<List<Cart>> GetCartsAsync()
        {
            return await dbContext.Carts.ToListAsync();
        }
        internal async Task<Cart> GetCartByIdAsync(int id)
        {
            return await dbContext.Carts.FirstOrDefaultAsync(c => c.Id == id);
        }
        internal async Task<Cart> GetCartByIpAsync(string ip)
        {
            return await dbContext.Carts.FirstOrDefaultAsync(c => c.IpAddress == ip);
        }
        internal async Task SaveCartAsync(Cart cart)
        {
            await dbContext.Carts.AddAsync(cart);
            await dbContext.SaveChangesAsync();
        }
        internal async Task UpdateCartAsync(Cart cart)
        {
            dbContext.Carts.Update(cart);
            await dbContext.SaveChangesAsync();
        }
        internal async Task DeleteCartAsync(int id)
        {
            var cart = await dbContext.Carts.FindAsync(id);
            dbContext.Remove(cart);
            await dbContext.SaveChangesAsync();
        }
    }
}
