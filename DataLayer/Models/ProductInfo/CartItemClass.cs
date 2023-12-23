using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.ProductInfo
{
    public class CartItemClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }

        // Shipping
        public double PackagedWeight { get; set; }
        public double PackagedDimensions { get; set; }

        // Digital
        public bool DigitalOnly { get; set; }

        // Fkeys
        [ForeignKey(nameof(StyleId))]
        public int StyleId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
    }
}
