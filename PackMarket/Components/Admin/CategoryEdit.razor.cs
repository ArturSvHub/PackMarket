using Blazorise;

using Microsoft.AspNetCore.Components;

using PackMarket.Data.Models;
using PackMarket.Extentions;
using PackMarket.Services;

using System.IO;

namespace PackMarket.Components.Admin
{
    public partial class CategoryEdit
    {
        [Parameter] public EventCallback UpdateState { get; set; }
        [Parameter] public Category? SelectedCategory { get; set; }
        [Parameter] public List<Category>? Categories { get; set; }
        [Parameter] public DataCrudService? DataContext { get; set; }
        private List<IFileEntry>? files;
        bool isFilesChecked = false;
        string oldPath;
        private List<Category>? ParentCategories { get; set; }
        public Modal? ModalRef;
        TimeSpan UtcLabel = new TimeSpan(3, 0, 0);
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }
        Task ShowModal()
        {
            if (ModalRef != null ||SelectedCategory!=null || SelectedCategory!.Id != 0)
            {
                oldPath = Path.Combine(Environment.CurrentDirectory,"wwwroot",SelectedCategory.ImagesPath);
                return ModalRef.Show(); 
            }
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
        async Task SaveCategory()
        {
            if(SelectedCategory.ParentId==0)
            {
                SelectedCategory.ParentId = null;
                SelectedCategory.Parent = null;
            }
            
            SelectedCategory.ChangedAt = DateTime.UtcNow + UtcLabel;
            SelectedCategory.Url = SelectedCategory.Title.TranslateToUrl();
            SelectedCategory.ImagesPath = Path.Combine("img", "categories", SelectedCategory.Url);
            var newPath = Path.Combine(Environment.CurrentDirectory, "wwwroot", SelectedCategory.ImagesPath);
            if(!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (oldPath!=newPath)
            {
                Directory.Move(oldPath, newPath);
            }
            if(files!=null)
            {
                int i = 0;
                foreach (var file in files)
                {
                    var fullpath = Path.Combine(newPath, $"{SelectedCategory.Url}{i}{Path.GetExtension(file.Name)}");
                    i++;
                    await using (FileStream fs = new(fullpath, FileMode.Create))
                    {
                        await file.OpenReadStream(long.MaxValue).CopyToAsync(fs);
                    }
                }
            }
            await DataContext.UpdateCategoryAsync(SelectedCategory);
            await UpdateState.InvokeAsync();
            await HideModal();
        }
    }
}
