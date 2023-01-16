using Microsoft.AspNetCore.Components;

using PackMarket.Data.Models;
using PackMarket.Extentions;
using PackMarket.Services;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PackMarket.Pages.Admin
{
    public partial class AdminProducts : ComponentBase
    {
        private TimeSpan UtcLabel = new(3, 0, 0);
        [Inject][Parameter] public DataCrudService CrudService { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Category> Categories { get; set; } = new List<Category>();

        private Product selectedProduct = new Product();
        protected override async Task OnInitializedAsync()
        {
            Products = await CrudService.GetProducts();
            Categories = await CrudService.GetCategories();
            tempcat = Categories.FirstOrDefault();
            await base.OnInitializedAsync();
        }
        async Task OnRowInserted(Product product,int catid)
        {
            if(catid!= null)
            {
                product.CategoryId = catid;
            }
            else
            {
                throw new NullReferenceException();
            }
            if (product.Id == 0)
            {
                product.CreatedAt = DateTime.UtcNow + UtcLabel;
                product.Url = product.Name.TranslateToUrl();
                await CrudService.Saveproduct(product);
            }
            else
            {
                product.ChangedAt = DateTime.UtcNow + UtcLabel;
                product.Url = product.Name.TranslateToUrl();
                await CrudService.UpdateProduct(product);
            }

            Products = await CrudService.GetProducts();
            StateHasChanged();
        }
        async Task OnRowRemoved(int id)
        {
            await CrudService.DeleteProduct(id);
            Products = await CrudService.GetProducts();
            StateHasChanged();
        }
        void OnNewItemDefaultSetter(Product product)
        {
            product = new Product { CreatedAt = DateTime.UtcNow + UtcLabel };
        }
    }
}
