using AuthReadyAPI.DataLayer.Models.Companies;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StyleSeeder : IEntityTypeConfiguration<StyleClass>
{
    public void Configure(EntityTypeBuilder<StyleClass> builder)
    {
        builder.HasData
            (
                new StyleClass
                {
                    Id = 1,
                    Name = "Rosa Parks",
                    Description = "Barbie Signature Rosa Parks Civil Rights Activist Inspiring Women Series",
                    CurrentPrice = 9.99,
                    CartCount = 0,
                    OrderCount = 0,
                    GrossIncome = 0.00,
                    IsAvailableForOrder = true,
                    IsLimitedTimeOnly = false,
                    UnavailableOn = DateTime.Now,
                    DiscountedPrice = 8.99,
                    UseDiscountPrice = false,
                    DigitalOnly = false,
                    PackagedDimensions = 99,
                    PackagedWeight = 1.50,
                    ProductId = 1,
                    CompanyId = 1,
                    DigitalOwnerId = 0
                }
            );
    }
}
