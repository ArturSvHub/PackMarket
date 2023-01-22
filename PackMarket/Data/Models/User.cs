using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations.Schema;

namespace PackMarket.Data.Models
{
    public class User:IdentityUser
    {
        public string? FirstName { get; set; }
        public int? CartId { get; set; }
        [ForeignKey("CartId")]
        public Cart? Cart { get; set; }
        public int? PromoId { get; set; }
        [ForeignKey("PromoId")]
        public Promo? Promo { get; set; }
    }
}