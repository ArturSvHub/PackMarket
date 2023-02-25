using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PackMarket.Data.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [JsonIgnore]
        public List<Product> Products { get; set; } = new();
    }
}
