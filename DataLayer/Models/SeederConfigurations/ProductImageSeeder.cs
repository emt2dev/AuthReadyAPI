using AuthReadyAPI.DataLayer.Models.Companies;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthReadyAPI.DataLayer.Models.SeederConfigurations
{
    public class ProductImageSeeder : IEntityTypeConfiguration<ProductImageClass>
    {
        public void Configure(EntityTypeBuilder<ProductImageClass> builder)
        {
            builder.HasData
                (
                 new ProductImageClass
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
