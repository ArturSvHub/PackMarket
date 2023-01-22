using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PackMarket.Components.Admin
{
    public partial class ProductEdit
    {
        [Parameter]
        public int Id { get; set; }
    }
}
