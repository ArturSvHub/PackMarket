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
    public partial class PriceList
    {
        [Inject] public DataCrudService CrudService { get; set; }
        public List<Product> Products { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Products= await CrudService.GetProductsAsync();
        }
    }
}