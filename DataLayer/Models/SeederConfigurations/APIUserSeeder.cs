using AuthReadyAPI.DataLayer.Models.PII;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthReadyAPI.DataLayer.Models.SeederConfigurations
{
    public class APIUserSeeder
    {
        public readonly UserManager<APIUserClass> userManager;

        public APIUserSeeder(UserManager<APIUserClass> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> Seed()
        {
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
            };

            var Attempt = await userManager.CreateAsync(TestUser, "P@ssword1");
            if (!Attempt.Succeeded) return false;

            await userManager.AddToRoleAsync(TestUser, "User");

            APIUserClass TestCompany = new APIUserClass
            {
                Name = "Test Company User",
                Email = "company@testcom",
                NormalizedEmail = "COMPANY@TEST.COM",
                NormalizedUserName = "COMPANY@TEST.COM",
                UserName = "company@test.com",
                FullName = "Test Company User",
                BillingAddress = "1239 Main St, Lifetime, USA, 33009",
                MailingAddress = "1239 Main St, Lifetime, USA, 33009",
                LifetimeSpent = 0.00,
                OrderCount = 0,
            };

            Attempt = await userManager.CreateAsync(TestCompany, "P@ssword1");
            if (!Attempt.Succeeded) return false;

            await userManager.AddToRoleAsync(TestCompany, "Company");

            APIUserClass TestAdmin = new APIUserClass
            {
                Name = "Test Admin User",
                Email = "admin@testcom",
                NormalizedEmail = "ADMIN@TEST.COM",
                NormalizedUserName = "ADMIN@TEST.COM",
                UserName = "admin@test.com",
                FullName = "Test Admin User",
                BillingAddress = "1239 Main St, Lifetime, USA, 33009",
                MailingAddress = "1239 Main St, Lifetime, USA, 33009",
                LifetimeSpent = 0.00,
                OrderCount = 0,
            };

            Attempt = await userManager.CreateAsync(TestCompany, "P@ssword1");
            if (!Attempt.Succeeded) return false;

            await userManager.AddToRoleAsync(TestCompany, "Admin");

            return true;
        }
    }
}
