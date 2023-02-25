using Blazored.LocalStorage;

using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using PackMarket.Data.Models;
using PackMarket.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace PackMarket.Pages.Cart
{
    public partial class Cart : ComponentBase
    {
        [Inject] public BlazorAppContext AppContext { get; set; }
        [Inject] public DataCrudService CrudService { get; set; }
        [Inject] public NavigationManager? NavigationManager { get; set; }
        Data.Models.Cart ThisCart;
        List<CartProduct> cartProducts;
        List<Product> Products=new();
        decimal? summary=0;
        protected override async Task OnInitializedAsync()
        {
            ThisCart = await CrudService.GetCartByIpAsync(AppContext.IP);
            if (ThisCart != null)
            {
                if (!string.IsNullOrEmpty(ThisCart.CartProducts))
                {
                    cartProducts = JsonSerializer.Deserialize<List<CartProduct>>(ThisCart.CartProducts);
                }
                for (int i = 0; i < cartProducts.Count; i++)
                {
                    Products.Add(await CrudService.GetProductByIdAsync(cartProducts[i].ProductId));
                    Products[i].Count = cartProducts[i].Count;
                    summary += (Products[i].Count * Products[i].RetailPrice);
                }
            }
        }
        private void ToCatalog()
        {
            NavigationManager!.NavigateTo("catalog");
        }
        private async Task ValueChangedAsync(int id,int count)
        {
            foreach (var item in cartProducts)
            {
                if(item.ProductId == id)
                {
                    item.Count= count;
                }
            }
            cartProducts.Sort();
            ThisCart.CartProducts = JsonSerializer.Serialize(cartProducts);
            await CrudService.UpdateCartAsync(ThisCart);
            StateHasChanged();
        }
        private async Task DeleteCart()
        {
            if(ThisCart!= null)
            {
                if (ThisCart.Id != 0)
                {
                    await CrudService.DeleteCartAsync(ThisCart.Id);
                }
            }
            NavigationManager.NavigateTo("");
        }
    }
}
