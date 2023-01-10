using Microsoft.AspNetCore.Identity;

namespace PackMarket.Data.Models
{
    public class User:IdentityUser
    {
        public string? FirstName { get; set; }
    }
}