using AuthReadyAPI.DataLayer.Models.Companies;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using CloudinaryDotNet.Actions;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthReadyAPI.DataLayer.Models.SeederConfigurations
{
    public class APIUserSeeder
    {
        public static async Task Seed(AuthDbContext context, UserManager<APIUserClass> userManager)
        {
            var Exists = await userManager.FindByEmailAsync("admin@test.com");
            if (Exists is not null) return;

            APIUserClass Admin = new APIUserClass
            {
                Id = "adminone",
                Name = "admin",
                NormalizedEmail = "ADMIN@TEST.COM",
                Email = "admin@test.com",
                UserName = "admin@test.com",
                NormalizedUserName = "ADMIN@TEST.COM",
                BillingAddress = "123 Main St, Anytown, USA 09832",
                MailingAddress = "123 Main St, Anytown, USA 09832",
                FullName = "admin",
                LifetimeSpent = 0.00,
                OrderCount = 0,
            };

            await userManager.CreateAsync(Admin, "P@ssword1");
            await userManager.AddToRoleAsync(Admin, "Admin");
            await userManager.UpdateAsync(Admin);

            context.ChangeTracker.Clear();

            APIUserClass Company = new APIUserClass
            {
                Id = "coone",
                Name = "company",
                NormalizedEmail = "COMPANY@TEST.COM",
                Email = "company@test.com",
                UserName = "company@test.com",
                NormalizedUserName = "COMPANY@TEST.COM",
                BillingAddress = "123 Main St, Anytown, USA 09832",
                MailingAddress = "123 Main St, Anytown, USA 09832",
                FullName = "company",
                LifetimeSpent = 0.00,
                OrderCount = 0,
            };


            await userManager.CreateAsync(Company, "P@ssword1");
            await userManager.AddToRoleAsync(Company, "Company");
            await userManager.UpdateAsync(Company);

            context.ChangeTracker.Clear();

            APIUserClass User = new APIUserClass
            {
                Id = "userone",
                Name = "user",
                NormalizedEmail = "USER@TEST.COM",
                Email = "user@test.com",
                UserName = "user@test.com",
                NormalizedUserName = "USER@TEST.COM",
                BillingAddress = "123 Main St, Anytown, USA 09832",
                MailingAddress = "123 Main St, Anytown, USA 09832",
                FullName = "user",
                LifetimeSpent = 0.00,
                OrderCount = 0,
            };

            await userManager.CreateAsync(User, "P@ssword1");
            await userManager.AddToRoleAsync(User, "User");
            await userManager.UpdateAsync(User);

            context.ChangeTracker.Clear();
        }
    }
}
