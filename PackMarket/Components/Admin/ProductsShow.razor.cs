using Blazorise;

using Microsoft.AspNetCore.Components;

using PackMarket.Data.Models;
using PackMarket.Extentions;
using PackMarket.Services;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PackMarket.Components.Admin
{
    public partial class ProductsShow : ComponentBase
    {
        //--------------------------------
        private TimeSpan UtcLabel = new(3, 0, 0);
        [Inject][Parameter] public DataCrudService CrudService { get; set; }
        List<Category> Categories { get; set; } = new List<Category>();
        List<Tag> Tags { get; set; } = new List<Tag>();
        List<State> States { get; set; } = new List<State>();
        public List<Product> Products { get; set; } = new List<Product>();
        private Product selectedProduct = new Product();
        protected override async Task OnInitializedAsync()
        {
            Categories = await CrudService.GetCategoriesAsync();
            Products = await CrudService.GetProductsTagsStatesAsync();
            Tags = await CrudService.GetTagsAsync();
            States= await CrudService.GetStatesAsync();
            await base.OnInitializedAsync();
        }
        async Task OnRowRemoved(Product product)
        {
            string path;
            if (product.ImagesPath != null)
            {
                path = Path.Combine(Environment.CurrentDirectory, "wwwroot", product.ImagesPath);
                if (Directory.Exists(path))
                {
                    try
                    {
                        Directory.Delete(path, true);
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            await CrudService.DeleteProductAsync(product.Id);
            Products = await CrudService.GetProductsAsync();
            StateHasChanged();
        }
        void OnNewItemDefaultSetter(Product product)
        {
            product = new Product { CreatedAt = DateTime.UtcNow + UtcLabel };
        }
        async Task ChangeProducts()
        {

            Products = await CrudService.GetProductsAsync();
            StateHasChanged();
        }
    }
}
