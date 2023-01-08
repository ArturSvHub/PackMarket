using Microsoft.AspNetCore.Identity;

namespace PackMarket.Data.Models
{
    public class Role:IdentityRole
    {

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
