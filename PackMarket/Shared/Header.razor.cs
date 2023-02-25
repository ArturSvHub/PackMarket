using Microsoft.AspNetCore.Components;

using PackMarket.Data.Models;
using PackMarket.Services;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PackMarket.Shared
{
    public partial class Header : ComponentBase
    {
        [Inject] BlazorAppContext BlazorAppContext { get; set; }
        [Inject] public RepositoryService Repository { get; set; }
        [Parameter] public List<Product> CartProducts { get; set; }
        int count = 0;
        protected override void OnInitialized()
        {
            if(CartProducts != null)
            {
                count = CartProducts.Count;
            }
        }
    }
}
