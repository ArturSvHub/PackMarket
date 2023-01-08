using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PackMarket.Shared
{
    public partial class Wrapper : ComponentBase
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
    }
}
