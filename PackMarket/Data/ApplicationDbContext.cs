using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using PackMarket.Data.Models;

namespace PackMarket.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionList> OptionLists { get; set; }
        public DbSet<Promo> Promos { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<State> States { get; set; }
    }
}