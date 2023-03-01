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
    public partial class ProductCreate
    {
        [Parameter] public EventCallback UpdateState { get; set; }
        [Parameter] public List<Category> Categories { get; set; }
        [Parameter] public DataCrudService DataContext { get; set; }
        [Parameter] public List<Tag> Tags { get; set; }
        [Parameter] public List<State> States { get; set; }
        public IReadOnlyList<int> selectedTagsIds;
        private bool isDisabled=true;
        private Product? Product { get; set; }
        public Modal? ModalRef;
        TimeSpan UtcLabel = new TimeSpan(3, 0, 0);
        List<IFileEntry?> files = new List<IFileEntry?>();
        //-------------
        protected override async Task OnInitializedAsync()
        {
            Product = new Product();
            selectedTagsIds = new List<int>();
        }
        //--------------
        Task ShowModal()
        {
            if (ModalRef != null)
            { return ModalRef.Show(); }
            else { return Task.CompletedTask; }
        }
        Task HideModal()
        {
            if (ModalRef != null)
            { return ModalRef.Hide(); }
            else { return Task.CompletedTask; }
        }
        Task OnFiles(FileChangedEventArgs e)
        {
            files = e.Files.ToList();
            return Task.CompletedTask;
        }
        async Task SaveProduct()
        {
            Product.CreatedAt = DateTime.UtcNow + UtcLabel;
            if (Product.State == null)
            {
                Product.StateId = 1;
            }
            Product.Tags = new List<Tag>();
            foreach (var id in selectedTagsIds)
            {
                Product.Tags.Add(Tags.FirstOrDefault(t => t.Id == id));
            }
            Product.Url = Product.Name.TranslateToUrl();
            Product.ImagesPath = Path.Combine("img", "products", Product.Url);
            string path = Path.Combine(Environment.CurrentDirectory, "wwwroot", Product.ImagesPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                int i = 0;
                foreach (var file in files)
                {
                    var fullpath = Path.Combine(path, $"{Product.Url}{i}{Path.GetExtension(file.Name)}");
                    i++;
                    await using (FileStream fs = new(fullpath, FileMode.Create))
                    {
                        await file.OpenReadStream(long.MaxValue).CopyToAsync(fs);
                    }
                }
                await DataContext.SaveProductAsync(Product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Product = new();
            files = new();
            await UpdateState.InvokeAsync();
            await HideModal();
        }
        private Task SelectedValueChanged(int value)
        {
            Product.CategoryId = value;
            isDisabled= false;
            return Task.CompletedTask;
        }
    }
}
