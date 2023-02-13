using Microsoft.AspNetCore.Components;

using PackMarket.Data.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PackMarket.Components
{
    public partial class ProductCard : ComponentBase
    {
        [Parameter] public Product Product { get; set; }
        [Parameter] public DirectoryInfo Directory { get; set; }
        public string ImagePath { get; set; }
        protected override void OnInitialized()
        {
            var file = Directory.GetFiles().FirstOrDefault();
            ImagePath = $"img/products/{Product.Url}/{file.Name}";
        }
    }
}
