using Blazorise.DataGrid;
using Blazorise.Extensions;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

using PackMarket.Data;
using PackMarket.Data.Models;
using PackMarket.Extentions;
using PackMarket.Services;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PackMarket.Pages.Admin
{
    public partial class AdminCategories : ComponentBase
    {
        string title;
        private TimeSpan UtcLabel = new(3, 0, 0); 
        [Inject][Parameter] public DataCrudService CrudService { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        private Category selectedCategory = new Category();
        protected override async Task OnInitializedAsync()
        {
            Categories = await CrudService.GetCategories();
            await base.OnInitializedAsync();
        }
        async Task OnRowInserted(Category category,int? parent=null)
        {
            category.ParentId=parent;
            if(category.Id== 0)
            {
                category.CreatedAt = DateTime.UtcNow + UtcLabel;
                category.Url = category.Title.TranslateToUrl();
                category.ChangedAt = DateTime.UtcNow + UtcLabel;
                await CrudService.SaveCategory(category);
            }
            else
            {
                category.Url = category.Title.TranslateToUrl();
                category.ChangedAt = DateTime.UtcNow + UtcLabel;
                await CrudService.UpdateCategory(category);
            }
            
            Categories = await CrudService.GetCategories();
            StateHasChanged();
        }
        async Task OnRowRemoved(int id)
        {
            await CrudService.DeleteCategory(id);
            Categories = await CrudService.GetCategories();
            StateHasChanged();
        }
        void OnNewItemDefaultSetter(Category category)
        {
            category = new Category { CreatedAt = DateTime.UtcNow + UtcLabel };
        }
    }
}
