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
    public partial class Catalog
    {
        protected override async Task OnInitializedAsync()
        {
            Categories = await DbContext.GetCategoriesAndProductsAsync();
        }
        [Inject]DataCrudService DbContext { get; set; }
        public List<Category> Categories { get; set; }
    }
}
