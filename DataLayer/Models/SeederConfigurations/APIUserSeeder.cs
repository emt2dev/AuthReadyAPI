using AuthReadyAPI.DataLayer.Models.PII;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthReadyAPI.DataLayer.Models.SeederConfigurations
{
    public class APIUserSeeder
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData
                (
                    APIUserClass TestUser = new APIUserClass 
                    {
                        Name = "Test User",
                        Email = "user@testcom",
                        NormalizedEmail = "USER@TEST.COM",
                        NormalizedUserName = "USER@TEST.COM",
                        UserName = "user@test.com",
                        FullName = "Test User",
                        BillingAddress = "1239 Main St, Lifetime, USA, 33009",
                        MailingAddress = "1239 Main St, Lifetime, USA, 33009",
                        LifetimeSpent = 0.00,
                        OrderCount = 0,
                    }
                );
    }
}
