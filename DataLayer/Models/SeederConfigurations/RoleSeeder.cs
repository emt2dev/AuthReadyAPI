using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthReadyAPI.DataLayer.Models.SeederConfigurations
{
    public class RoleSeeder : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                    new IdentityRole
                    {
                        Name = "Developer",
                        NormalizedName = "DEVELOPER"
                    },
                    new IdentityRole
                    {
                        Name = "Customer",
                        NormalizedName = "CUSTOMER"
                    },
                    new IdentityRole
                    {
                        Name = "Owner",
                        NormalizedName = "OWNER"
                    },
                    new IdentityRole
                    {
                        Name = "Staff",
                        NormalizedName = "STAFF"
                    }
                );
        }
    }
}
