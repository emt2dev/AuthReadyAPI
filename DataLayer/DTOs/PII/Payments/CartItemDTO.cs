using AuthReadyAPI.DataLayer.Models.ProductInfo;

namespace AuthReadyAPI.DataLayer.DTOs.PII
{
    public class CartItemDTO
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
        public int StyleId { get; set; }
        public int ProductId { get; set; }
        public int CompanyId { get; set; }
    }
}