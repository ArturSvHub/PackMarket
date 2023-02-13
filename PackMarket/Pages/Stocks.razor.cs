using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

using PackMarket.Data.Models;
using PackMarket.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PackMarket.Pages
{
    public partial class Stocks
    {
        [Inject] DataCrudService DbContext { get; set; }
        public List<Product> Products { get; set; }
        DirectoryInfo ProductsDirectory = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "wwwroot", "img", "products"));
        List<DirectoryInfo> Directories = new List<DirectoryInfo>();

        protected override async Task OnInitializedAsync()
        {
            var tag = await DbContext.GetTagByNameAsync("stock");
            Products = await DbContext.GetProductsTagsAsync();
            Products = Products.Where(p => p.Tags.Contains(tag)).ToList();
            foreach (var item in Products)
            {
                Directories.Add(new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "wwwroot", "img", "products", item.Url)));
            }
        }
    }
}
