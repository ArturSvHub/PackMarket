using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;
using NuGet.Protocol;
using PackMarket.Data.Models;
using PackMarket.Data.ViewModels;
using PackMarket.Services;

namespace PackMarket.Pages
{
    public partial class Index:ComponentBase
    {
        string name = "product";
        Product tp;
        string temp = "";

        [Inject]public RepositoryService Repository { get; set; }
        [Inject] public IJSRuntime? jsRuntime{ get; set; }
        [Parameter] public List<Category> Categories { get; set; }
        public List<Category> MainCategories { get; set; }
        string ip;

        protected override void OnInitialized()
        {
            MainCategories = Repository.Categories.Where(c => c.Parent == null).ToList();
            foreach (var category in MainCategories)
            {
                if(category.Products.Count>0)
                {
                    temp = JsonSerializer.Serialize(category.Products.First());
                    return;
                }
            }
        }

        public async Task<string> GetIpAddress()
        {
            try
            {
                var ipAddress = await jsRuntime.InvokeAsync<string>("getIpAddress").ConfigureAwait(true);
                return ipAddress;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
