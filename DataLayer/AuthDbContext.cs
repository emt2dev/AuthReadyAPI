using AuthReadyAPI.DataLayer.Models.Companies;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using AuthReadyAPI.DataLayer.Models.SeederConfigurations;
using AuthReadyAPI.DataLayer.Models.ServicesInfo;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer
{
    public class AuthDbContext : IdentityDbContext<APIUserClass>
    {
        public AuthDbContext(DbContextOptions options) : base(options) { }

        public DbSet<CompanyClass> Companies { get; set; }
        public DbSet<PointOfContactClass> PointOfContacts { get; set; }
        public DbSet<OrderClass> Orders { get; set; }
        public DbSet<ProductClass> Products { get; set; }
        public DbSet<CartItemClass> CartItems { get; set; }
        public DbSet<ProductImageClass> ProductImages { get; set; }
        public DbSet<CompanyImageClass> CompanyImages { get; set; }
        public DbSet<ShippingInfoClass> ShippingInfo { get; set; }
        public DbSet<StyleClass> Styles { get; set; }
        public DbSet<ShoppingCartClass> ShoppingCarts { get; set; }
        public DbSet<CategoryClass> Categories { get; set; }
        public DbSet<SingleProductClass> SingleProducts { get; set; }
        public DbSet<SingleProductCartClass> SingleProductCarts { get; set; }
        public DbSet<AuctionProductCartClass> AuctionCarts { get; set; }
        public DbSet<AuctionProductClass> AuctionProducts { get; set; }
        public DbSet<SingleProductImageClass> SingleProductImages { get; set; }
        public DbSet<AuctionProductImageClass> AuctionProductImages { get; set; }
        public DbSet<BidClass> Bids { get; set; }
        public DbSet<ServicesClass> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* Role and User Seeder */
            modelBuilder.ApplyConfiguration(new RoleSeeder()); // seeds the roles table
        }
    }
}
