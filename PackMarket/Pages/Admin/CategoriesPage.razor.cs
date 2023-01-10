using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PackMarket.Data.Models;
using PackMarket.Data;
using PackMarket.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using static MudBlazor.CategoryTypes;
using MudBlazor;
using PackMarket.Extentions;

namespace PackMarket.Pages.Admin
{
    public partial class CategoriesPage
    {
        [Inject][Parameter]
        public MarketContext Context { get; set; }
        List<Category> Categories { get; set; }
        private string _searchString;
        private bool _sortNameByLength;
        private List<string> _events = new();
        protected override async Task OnInitializedAsync()
        {
            Categories = await Context.DbContext.Categories.ToListAsync();
        }
        private Func<Category, object> _sortBy => x =>
        {
            if (_sortNameByLength)
                return x.Title.Length;
            else
                return x.Title;
        };
        private Func<Category, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Url.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Title.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if ($"{x.Title} {x.Url}".Contains(_searchString))
                return true;

            return false;
        };
        void RowClicked(DataGridRowClickEventArgs<Category> args)
        {
            _events.Insert(0, $"Event = RowClick, Index = {args.RowIndex}, Data = {System.Text.Json.JsonSerializer.Serialize(args.Item)}");
        }

        void SelectedItemsChanged(HashSet<Category> items)
        {
            _events.Insert(0, $"Event = SelectedItemsChanged, Data = {System.Text.Json.JsonSerializer.Serialize(items)}");
        }
        async Task CreateFake()
        {
            StringTransliterate tr = new();
            List<string> strings = new List<string> { "Скотч", "Стретч" ,"Пленка","Коробки","Прочее"};
            List<Category> categories = new List<Category>();
            foreach (var item in strings)
            {
                categories.Add(new Category { Title= item,Url= tr.TranslateToUrl(item)});
            }
            await Context.DbContext.AddRangeAsync(categories);
            await Context.DbContext.SaveChangesAsync();
            Categories = await Context.DbContext.Categories.ToListAsync();
        }
        async Task DeleteFake()
        {
            Context.DbContext.Categories.RemoveRange(await Context.DbContext.Categories.ToListAsync());
            await Context.DbContext.SaveChangesAsync();
            Categories = await Context.DbContext.Categories.ToListAsync();
        }
    }
}
