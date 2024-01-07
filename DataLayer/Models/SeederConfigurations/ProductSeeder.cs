using AuthReadyAPI.DataLayer.Models.ProductInfo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthReadyAPI.DataLayer.Models.SeederConfigurations
{
    public class ProductSeeder
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData
                (
                    new CategoryClass
                    {
                        Id = 1,
                        Name = "Toy",
                        CompanyId = 1
                    },
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
                    }, new StyleClass
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
                    }, new ProductImageClass
                    {
                        Id = 1,
                        ProductId = 1,
                        StyleId = 1,
                        ImageUrl = "https://www.ebay.com/itm/325914851835?chn=ps&norover=1&mkevt=1&mkrid=711-117182-37290-0&mkcid=2&mkscid=101&itemid=325914851835&targetid=1531876732278&device=c&mktype=pla&googleloc=9008533&poi=&campaignid=19851828444&mkgroupid=145880009014&rlsatarget=pla-1531876732278&abcId=9307249&merchantid=101491518&gclid=Cj0KCQiAkeSsBhDUARIsAK3tiefJewYtYyA-3-PuCb51Ogy66tVAz1RAXPLNkQBGFKr6BWTdqSarwPEaArz0EALw_wcB"
                    }, new ProductImageClass
                    {
                        Id = 2,
                        ProductId = 1,
                        StyleId = 1,
                        ImageUrl = "https://i.ebayimg.com/images/g/rrIAAOSwITZdZAJM/s-l960.jpg"
                    }



                );
        }
    }
}
