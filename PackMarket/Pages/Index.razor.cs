using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using PackMarket.Data.Models;
using PackMarket.Data.ViewModels;
using PackMarket.Services;

namespace PackMarket.Pages
{
    public partial class Index:ComponentBase
    {
        [Inject]public RepositoryService Repository { get; set; }
        [Inject] public IJSRuntime? jsRuntime{ get; set; }
        [Parameter] public List<Category> Categories { get; set; }
        public List<Category> MainCategories { get; set; }
        string ip;

        protected override void OnInitialized()
        {
            MainCategories = Repository.Categories.Where(c => c.Parent == null).ToList();
            
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
