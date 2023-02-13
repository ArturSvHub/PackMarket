using System.ComponentModel.DataAnnotations;

namespace PackMarket.Data.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
