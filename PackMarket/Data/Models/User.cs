using Microsoft.AspNetCore.Identity;

namespace PackMarket.Data.Models
{
    public class User:IdentityUser
    {
        public string? FirstName { get; set; }
        public virtual ICollection<IdentityUserClaim<string>>? Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>>? Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>>? Tokens { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}