using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PackMarket.Components.IndexComponents
{
    public partial class IndexCard : ComponentBase
    {
        [Parameter] public string ImagePath { get; set; }
        [Parameter] public string Caption { get; set; }
        [Parameter] public string Href { get; set; }
    }
}
