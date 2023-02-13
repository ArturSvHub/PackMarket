using PackMarket.Data.Models;

namespace PackMarket.Services
{
    public class RepositoryService
    {
        private readonly DataCrudService dbContext;
        public RepositoryService(DataCrudService dbContext)
        {
            this.dbContext = dbContext;
            Categories = Categories ?? Task.Run(async () => await GetCategories()).Result;
        }
        public List<Category> Categories { get; set; }
        public async Task<List<Category>> GetCategories()
        {
            return await dbContext.GetCategoriesAndProductsAsync();
        }
    }
}
