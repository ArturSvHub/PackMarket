using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using PackMarket.Data.ViewModels;
using PackMarket.Services;

namespace PackMarket.Pages
{
    public partial class Index:ComponentBase
    {
        //[Inject]public DataCrudService DbContext { get; set; }
        [Inject] public IJSRuntime? jsRuntime{ get; set; }
        public PageView Page { get; set; } = new();

        string ip;
        protected override async Task OnInitializedAsync()
        {
            ip = await GetIpAddress();
            //Page.Categories = await DbContext.GetCategoriesAndProductsAsync();
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
