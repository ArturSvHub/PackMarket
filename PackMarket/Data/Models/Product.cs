using PackMarket.Data.Enums;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PackMarket.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? Description { get; set; }
        public decimal? PurshasePrice { get; set; }
        public decimal? RetailPrice { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ChangedAt { get; set; }
        public State State { get; set; }
        public List<Tag> Tags { get; set; }=new List<Tag>();
        public int CategoryId { get; set; }
        [Required]
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public string? ImagesPath { get; set; }
    }
}
