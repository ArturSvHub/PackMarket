using Microsoft.AspNetCore.Components;

using PackMarket.Data.Models;
using PackMarket.Data.ViewModels;
using PackMarket.Services;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PackMarket.Pages
{
    public partial class PageCategory : ComponentBase
    {
        [Inject]NavigationManager Navigation {  get; set; }
        [Inject]DataCrudService DbContext { get; set; }
        [Parameter] public string PageUri { get; set; }
        public Category Category { get; set; }
        public List<Category> Children { get; set; }
        public List<CatalogView> Catalog { get; set; } = new();
        public List<DirectoryInfo> directories =new();
        public FileInfo[] FileNames { get; set; }
        string catPath;
        protected override async Task OnInitializedAsync()
        {
            Category = await DbContext.GetCategoryByUrlAsync(PageUri);
            if(Category.Children!= null||Category.Children.Count>0)
            {
                Children = Category.Children.ToList();
                foreach (var item in Children)
                {
                    catPath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "img", "categories", item.Url!);
                    GetPath();
                    Catalog.Add(new CatalogView { CategoryTitle = item.Title, CategoryUrl = item.Url, CategoryImagePath = $"img/categories/{item.Url}/{FileNames[0].Name}" });
                }
            }
            if(Category.Products!= null||Category.Products.Count>0)
            {
                foreach (var prod in Category.Products)
                {
                    directories.Add(new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "wwwroot", "img", "products",prod.Url)));
                }
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
