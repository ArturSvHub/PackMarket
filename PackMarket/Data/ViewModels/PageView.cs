using PackMarket.Data.Models;

namespace PackMarket.Data.ViewModels
{
    public class PageView
    {
        public List<Category>? Categories { get; set; }
        public List<CategoryProduct>? CategoryProducts { get; set; }
        public List<Option>? Options { get; set; }
    }
    public record CategoryProduct(List<Category>? Categories, List<Product>? Products);
}
