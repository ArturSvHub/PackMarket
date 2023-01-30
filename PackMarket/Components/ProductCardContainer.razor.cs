using Microsoft.AspNetCore.Components;

using PackMarket.Data.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PackMarket.Components
{
    public partial class ProductCardContainer : ComponentBase
    {
        [Parameter] public List<Product> Products { get; set; }
        [Parameter] public FileInfo[] Files { get; set; }
    }
}
