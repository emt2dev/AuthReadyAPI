using AuthReadyAPI.DataLayer.Models.Companies;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthReadyAPI.DataLayer.Models.SeederConfigurations
{
    public class ProductSeeder : IEntityTypeConfiguration<ProductClass>
    {
        public void Configure(EntityTypeBuilder<ProductClass> builder)
        {
            builder.HasData
                (
                    new ProductClass
                    {
                        Id = 1,
                        Name = "Barbie Signature Collection",
                        Description = "Signature collection of barbie dolls.",
                        CreatedOn = DateTime.Now,
                        UpdatedOn = DateTime.Now,
                        UpdatesMade = "System Generated",
                        MainImageUrl = "https://creations.mattel.com/cdn/shop/products/ors1kicv0rkdf2teoqaf.png?v=1684560830",
                        Quantity = 1,
                        ViewCount = 0,
                        UpdatedBy = "0",
                        CompanyId = 1,
                        TaxCode = "tst_0009",
                        CategoryId = 1
                    }
                );
        }
    }
}
