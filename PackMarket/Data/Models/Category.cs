using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PackMarket.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Url { get; set; }
        public int? ParentId { get; set; }
        public int? Parent { get; set; }
        [ForeignKey("ParentId")]
        public ICollection<Category>? Children { get; set; } = new List<Category>();
        public ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
