using Blazorise;
using Microsoft.AspNetCore.Components;
using PackMarket.Data.Models;
using PackMarket.Extentions;
using PackMarket.Services;

namespace PackMarket.Components.Admin
{
    public partial class CategoryCreate
    {
        [Parameter] public EventCallback UpdateState { get; set; }
        [Parameter] public List<Category> Categories { get; set; }
        [Parameter] public DataCrudService DataContext { get; set; }
        private Category? Category { get; set; }
        public Modal? ModalRef;
        TimeSpan UtcLabel = new TimeSpan(3, 0, 0);
        List<IFileEntry?> files= new List<IFileEntry?>();
        //-------------
        protected override async Task OnInitializedAsync()
        {
            Category = new Category();
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
        async Task SaveCategory()
        {
            Category.CreatedAt = DateTime.UtcNow + UtcLabel;
            Category.Url = Category.Title.TranslateToUrl();
            Category.ImagesPath = Path.Combine("img","categories",Category.Url);
            string path = Path.Combine(Environment.CurrentDirectory,"wwwroot",Category.ImagesPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                int i = 0;
                foreach (var file in files)
                { 
                    var fullpath = Path.Combine(path, $"{Category.Url}{i}{Path.GetExtension(file.Name)}");
                    i++;
                    await using(FileStream fs = new(fullpath, FileMode.Create))
                    {
                        await file.OpenReadStream(long.MaxValue).CopyToAsync(fs);
                    }
                }
                await DataContext.SaveCategoryAsync(Category);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Category = new();
            files = new();
            await UpdateState.InvokeAsync();
            await HideModal();
        }
    }
}
