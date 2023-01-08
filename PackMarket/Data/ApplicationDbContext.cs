using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using PackMarket.Data.Models;

namespace PackMarket.Data
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,string,IdentityUserClaim<string>,UserRole,IdentityUserLogin<string>,IdentityRoleClaim<string>,IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>(b =>
            {
                b.HasMany(e => e.Claims)
                .WithOne().HasForeignKey(uc => uc.UserId)
                .IsRequired();

                b.HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

                b.HasMany(e => e.Tokens)
                .WithOne()
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

                b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });
            builder.Entity<Role>(b =>
            {
                b.HasMany(e => e.UserRoles).WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId).IsRequired();
            });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}