using Blazorise;

using Microsoft.AspNetCore.Components;

using PackMarket.Data.Models;
using PackMarket.Extentions;
using PackMarket.Services;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PackMarket.Components.Admin
{
    public partial class CategoriesShow : ComponentBase
    {
        private const string PREVIOUS = "previous";
        private const string NEXT = "next";
        private string currentPage = "1";
        private int pageItems = 5;

        private bool IsActive(string page)
            => currentPage == page;

        private bool IsPageNavigationDisabled(string navigation)
        {
            if (navigation.Equals(PREVIOUS))
            {
                return currentPage.Equals("1");
            }
            else if (navigation.Equals(NEXT))
            {
                return currentPage.Equals(pageItems.ToString());
            }
            return false;
        }

        private void Previous()
        {
            var currentPageAsInt = int.Parse(currentPage);
            if (currentPageAsInt > 1)
            {
                currentPage = (currentPageAsInt - 1).ToString();
            }
        }

        private void Next()
        {
            var currentPageAsInt = int.Parse(currentPage);
            if (currentPageAsInt < pageItems)
            {
                currentPage = (currentPageAsInt + 1).ToString();
            }
        }

        private void SetActive(string page)
            => currentPage = page;
        //--------------------------------
        private TimeSpan UtcLabel = new(3, 0, 0);
        [Inject][Parameter] public DataCrudService CrudService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        private Category selectedCategory = new Category();
        protected override async Task OnInitializedAsync()
        {
            Categories = await CrudService.GetCategoriesAsync();
            await base.OnInitializedAsync();
        }
        async Task OnRowInserted(Category category, int? parent = null)
        {
            category.ParentId = parent;
            if (category.Id == 0)
            {
                category.CreatedAt = DateTime.UtcNow + UtcLabel;
                category.Url = category.Title.TranslateToUrl();
                category.ChangedAt = DateTime.UtcNow + UtcLabel;
                await CrudService.SaveCategoryAsync(category);
            }
            else
            {
                category.Url = category.Title.TranslateToUrl();
                category.ChangedAt = DateTime.UtcNow + UtcLabel;
                await CrudService.UpdateCategoryAsync(category);
            }

            Categories = await CrudService.GetCategoriesAsync();
            StateHasChanged();
        }
        async Task OnRowRemoved(Category category)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "wwwroot", category.ImagesPath);
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, true);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            await CrudService.DeleteCategoryAsync(category.Id);
            Categories = await CrudService.GetCategoriesAsync();
            StateHasChanged();
        }
        void OnNewItemDefaultSetter(Category category)
        {
            category = new Category { CreatedAt = DateTime.UtcNow + UtcLabel };
        }
        public Modal EditModal;
        async Task ChangeCategories()
        {
            Categories = await CrudService.GetCategoriesAsync();
            StateHasChanged();
        }
    }
}
