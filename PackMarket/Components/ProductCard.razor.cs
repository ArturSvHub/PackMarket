using Microsoft.AspNetCore.Components;

using PackMarket.Data.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PackMarket.Components
{
    public partial class ProductCard : ComponentBase
    {
        [Parameter] public Product Product { get; set; }
        [Parameter] public string ImagePath { get; set; }
        
    }
}
