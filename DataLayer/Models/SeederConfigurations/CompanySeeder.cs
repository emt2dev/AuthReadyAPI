using AuthReadyAPI.DataLayer.Models.Companies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthReadyAPI.DataLayer.Models.SeederConfigurations
{
    public class CompanySeeder
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData
                (
                    new CompanyClass
                    {
                        Id = 1,
                        Active = true,
                        Name = "Test Company",
                        Description = "Test company located in fake city.",
                        PhoneNumber = "1 1234567890",
                        MailingAddress = "123 Main Street, Any Town, EE, USA 11111",
                        ShippingAddress = "123 Main Street, Any Town, EE, USA 11111",
                    }, new CompanyClass
                    {
                        Id = 1,
                        Active = true,
                        Name = "Test Company",
                        Description = "Test company located in fake city.",
                        PhoneNumber = "1 1234567890",
                        MailingAddress = "123 Main Street, Any Town, EE, USA 11111",
                        ShippingAddress = "123 Main Street, Any Town, EE, USA 11111",
                    }
                );
        }
    }
}
