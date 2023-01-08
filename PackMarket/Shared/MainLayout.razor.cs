using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using PackMarket;
using PackMarket.Shared;

namespace PackMarket.Shared
{
    public partial class MainLayout
    {
        
        bool _drawerOpen;
        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }
        
    }
}