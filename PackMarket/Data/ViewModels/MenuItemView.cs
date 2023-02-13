namespace PackMarket.Data.ViewModels
{
    public record MenuItemView
    {
        public string? CategoryTitle { get; set; }
        public string? CategoryUrl { get; set; }
        public string? ProductName { get; set; }
        public string? ProductUrl { get; set; }
    }
}
