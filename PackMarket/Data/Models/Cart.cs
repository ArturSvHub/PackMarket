using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PackMarket.Data.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public List<Product>? Products { get; set; }
        [NotMapped]
        public string? IpAddress { get; set; }
    }
}
