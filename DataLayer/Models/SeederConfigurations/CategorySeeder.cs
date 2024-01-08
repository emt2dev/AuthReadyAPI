using AuthReadyAPI.DataLayer.Models.Companies;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategorySeeder : IEntityTypeConfiguration<CategoryClass>
{
    public void Configure(EntityTypeBuilder<CategoryClass> builder)
    {
        builder.HasData
            (
                new CategoryClass
                {
                    Id = 1,
                    Name = "Toy",
                    CompanyId = 1
                },
                new CategoryClass
                {
                    Id = 2,
                    Name = "Shirt",
                    CompanyId = 1
                }
            );
    }
}
