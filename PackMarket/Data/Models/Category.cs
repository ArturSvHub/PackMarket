using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        public Category? Parent { get; set; }
        public int? OptionListId { get; set; }
        [ForeignKey("OptionListId")]
        public OptionList? OptionList { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ChangedAt { get; set; }
        [JsonIgnore]
        public ICollection<Category>? Children { get; set; } = new List<Category>();
        [JsonIgnore]
        public ICollection<Product>? Products { get; set; } = new List<Product>();
        public string? ImagesPath { get; set; }
    }
}
