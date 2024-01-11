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
                     ImageUrl = "https://wwd.com/wp-content/uploads/2023/06/MEGA989664_002.jpg"
                 }, new ProductImageClass
                 {
                     Id = 2,
                     ProductId = 1,
                     StyleId = 1,
                     ImageUrl = "https://www.becauseofthemwecan.com/wp-content/uploads/2023/08/FC542588-1509-4082-88D1-4B83DD3FF7DE-1024x683.jpeg"
                 }, new ProductImageClass
                 {
                     Id = 3,
                     ProductId = 2,
                     StyleId = 2,
                     ImageUrl = "https://i.etsystatic.com/34524380/r/il/4cfc2c/5539591259/il_794xN.5539591259_6216.jpg"
                 }, new ProductImageClass
                 {
                     Id = 4,
                     ProductId = 2,
                     StyleId = 3,
                     ImageUrl = "https://i.etsystatic.com/34524380/r/il/623606/5573591921/il_794xN.5573591921_oio0.jpg"
                 }                 
                );
        }
    }
}
