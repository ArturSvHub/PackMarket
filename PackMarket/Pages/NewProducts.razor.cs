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
        DirectoryInfo ProductsDirectory = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory,"wwwroot","img","products"));
        public FileInfo[] ProductFiles { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Products = await DbContext.GetProductsOrderedByUrlAsync();
            if(ProductsDirectory !=null||ProductsDirectory.Exists)
            {
                ProductFiles = ProductsDirectory.GetFiles("*", SearchOption.AllDirectories);
            }
        }
    }
}
