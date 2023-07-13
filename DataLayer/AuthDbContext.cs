using AuthReadyAPI.DataLayer.Models;
using AuthReadyAPI.DataLayer.Models.SeederConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace AuthReadyAPI.DataLayer
{
    public class AuthDbContext : IdentityDbContext<v2_UserStripe>
    {
        public AuthDbContext(DbContextOptions options) : base(options) { }

        public DbSet<v2_ProductStripe> v2_Products { get; set; }
        public DbSet<v2_Company> v2_Companies { get; set; }
        public DbSet<v2_Order> v2_Orders { get; set; }
        public DbSet<v2_UserStripe> v2_UserStripes { get; set; }
        public DbSet<v2_ShoppingCart> v2_ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* Role and User Seeder */
            modelBuilder.ApplyConfiguration(new RoleSeeder()); // seeds the roles table
        }
    }
}
