using Microsoft.AspNetCore.Components;

using PackMarket.Data.Models;
using PackMarket.Data.ViewModels;
using PackMarket.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace PackMarket.Components.IndexComponents
{
    public partial class MainCatalog : ComponentBase
    {
        [Parameter] public bool WithSidebar { get; set; }
        [Inject] public DataCrudService DbContext { get; set; }
        [Parameter] public List<Category> Categories { get; set; }
        public List<CatalogView> Catalog { get; set; } = new List<CatalogView>();
        public FileInfo[] FileNames { get; set; }
        string catPath;
        protected async override Task OnInitializedAsync()
        {
            foreach (var item in Categories)
            {
                catPath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "img", "categories", item.Url!);
                GetPath();
                Catalog.Add(new CatalogView { CategoryTitle = item.Title, CategoryUrl = item.Url, CategoryImagePath = $"img/categories/{item.Url}/{FileNames[0].Name}" });

            }
        }
        private void GetPath()
        {

            if (Directory.Exists(catPath))
            {
                var df = new DirectoryInfo(catPath);
                FileNames = df.GetFiles();
            }
            else
                FileNames = new FileInfo[0];
        }
    }
}
