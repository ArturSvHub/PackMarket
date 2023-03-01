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
    public partial class ProductEdit
    {
        [Parameter] public EventCallback UpdateState { get; set; }
        [Parameter] public List<Category> Categories { get; set; }
        [Parameter] public DataCrudService DataContext { get; set; }
        private bool isDisabled = false;
        [Parameter] public Product? SelectedProduct { get; set; }
        [Parameter]public List<Tag> Tags { get; set; }
        [Parameter]public List<State> States { get; set; }
        public IReadOnlyList<int> selectedTagsIds;
        public Modal? ModalRef;
        TimeSpan UtcLabel = new TimeSpan(3, 0, 0);
        List<IFileEntry?> files = new List<IFileEntry?>();
        //--------------
        protected override void OnInitialized()
        {
            if (SelectedProduct.Tags != null)
            {
                var ids = new List<int>();
                foreach (var item in SelectedProduct.Tags)
                {
                    ids.Add(item.Id);
                }
                selectedTagsIds = new List<int>(ids);
            }
            else
            {
                selectedTagsIds = new List<int>();
            }
            
        }

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
            SelectedProduct.ChangedAt = DateTime.UtcNow + UtcLabel;
            if (SelectedProduct.State == null)
            {
                SelectedProduct.StateId = 1;
            }
            SelectedProduct.Tags = new List<Tag>();
            foreach (var id in selectedTagsIds)
            {
                SelectedProduct.Tags.Add(Tags.FirstOrDefault(t=>t.Id==id));
            }
            SelectedProduct.Url = SelectedProduct.Name.TranslateToUrl();
            SelectedProduct.ImagesPath = Path.Combine("img", "products", SelectedProduct.Url);
            string path = Path.Combine(Environment.CurrentDirectory, "wwwroot", SelectedProduct.ImagesPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                int i = 0;
                foreach (var file in files)
                {
                    var fullpath = Path.Combine(path, $"{SelectedProduct.Url}{i}{Path.GetExtension(file.Name)}");
                    i++;
                    await using (FileStream fs = new(fullpath, FileMode.Create))
                    {
                        await file.OpenReadStream(long.MaxValue).CopyToAsync(fs);
                    }
                }
                await DataContext.UpdateProductAsync(SelectedProduct);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            await UpdateState.InvokeAsync();
            await HideModal();
        }
        private Task SelectedValueChanged(int value)
        {
            SelectedProduct.CategoryId = value;
            isDisabled = false;
            return Task.CompletedTask;
        }
        private Task SelectedStateChanged(int value)
        {
            SelectedProduct.StateId = value;
            return Task.CompletedTask;
        }
    }
}
