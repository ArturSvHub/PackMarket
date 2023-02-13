using Microsoft.AspNetCore.Components;

using PackMarket.Data.Models;
using PackMarket.Services;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PackMarket.Pages
{
    public partial class NewProducts : ComponentBase
    {
        [Inject] DataCrudService DbContext { get; set; }
        public List<Product> Products { get; set; }
        List<DirectoryInfo> Directories = new List<DirectoryInfo>();
        public FileInfo[] ProductFiles { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var tag = await DbContext.GetTagByNameAsync("new");
            Products = await DbContext.GetProductsTagsAsync();
            Products = Products.Where(p=>p.Tags.Contains(tag)).ToList();
            foreach (var item in Products)
            {
                Directories.Add(new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "wwwroot", "img", "products",item.Url)));
            }
        }
    }
}
