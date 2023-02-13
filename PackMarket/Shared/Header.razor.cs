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
        [Inject] public RepositoryService Repository { get; set; }
    }
}
