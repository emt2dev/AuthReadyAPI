using AuthReadyAPI.DataLayer.Models;
using AuthReadyAPI.DataLayer.Models.SeederConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace AuthReadyAPI.DataLayer
{
    public class AuthDbContext : IdentityDbContext<APIUser>
    {
        public AuthDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* Role and User Seeder */
            modelBuilder.ApplyConfiguration(new RoleSeeder()); // seeds the roles table
        }
    }
}
