using Microsoft.AspNetCore.Components;

using PackMarket.Data.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PackMarket.Components.CatalogComponents
{
    public partial class CatalogSideBar
    { 
        [Parameter] public List<Category> Categories { get; set; }
    }
}
