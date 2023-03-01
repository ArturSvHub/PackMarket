using Blazorise.Extensions;

using Microsoft.AspNetCore.Components;
using PackMarket.Data.Models;
using PackMarket.Services;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace PackMarket.Components
{
    public partial class ProductCard : ComponentBase
    {
        [Inject]DataCrudService DataCrud { get; set; }
        [Inject]BlazorAppContext BlazorAppContext { get; set; }
        public Data.Models.Cart Cart { get; set; }
        public List<CartProduct> CartProducts { get; set; }
        [Parameter] public Product Product { get; set; }
        [Parameter] public DirectoryInfo Directory { get; set; }
        public string ImagePath { get; set; }
        protected override void OnInitialized()
        {
            var file = Directory.GetFiles().FirstOrDefault();
            if(file == null)
            {
                ImagePath = $"img/products/";
            }
            else
            {
                ImagePath = $"img/products/{Product.Url}/{file.Name}";
            }
            Product.Count = 1;
        }
        private async Task ToCart()
        {
            Cart = await DataCrud.GetCartByIpAsync(BlazorAppContext.IP);
            if (Cart == null)
            {
                CartProducts = new List<CartProduct> { new CartProduct() { ProductId = Product.Id, Count = Product.Count } };
                Cart = new Data.Models.Cart {
                    IpAddress = BlazorAppContext.IP,
                    CreatedAt = DateTime.UtcNow + TimeSpan.FromHours(3),
                    CartProducts = JsonSerializer.Serialize(CartProducts)
                };
            }
            if (Cart.Id == 0)
            {
                await DataCrud.SaveCartAsync(Cart);
            }
            else
            {
                if(string.IsNullOrWhiteSpace(Cart.CartProducts))
                {
                    CartProducts = new List<CartProduct>();
                }
                else
                {
                    CartProducts = JsonSerializer.Deserialize<List<CartProduct>>(Cart.CartProducts)!;
                }
                for (int i = 0; i < CartProducts.Count; i++)
                {
                    if (CartProducts[i].ProductId==Product.Id)
                    {
                        CartProducts[i].Count += Product.Count;
                    }
                    else
                    {
                        CartProduct cp = new CartProduct() { ProductId = Product.Id, Count = Product.Count };
                        CartProducts.Add(cp);
                        break;
                    }
                }
                CartProducts.Sort();
                Cart.CartProducts = JsonSerializer.Serialize(CartProducts);
                await DataCrud.UpdateCartAsync(Cart);
            }
        }
    }
}
