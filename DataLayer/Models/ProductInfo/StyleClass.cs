using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.ProductInfo
{
    public class StyleClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double CurrentPrice { get; set; }

        // Metrics
        public int CartCount { get; set; }
        public int OrderCount { get; set; }
        public double GrossIncome { get; set; }

        // Urgency
        public bool IsAvailableForOrder { get; set; }
        public bool IsLimitedTimeOnly { get; set; }
        public DateTime UnavailableOn { get; set; }
        public bool IsComingSoon { get; set; }
        public DateTime AvailableOn { get; set; }
        public double DiscountedPrice { get; set; }
        public bool UseSalePrice { get; set; }
        public bool DigitalOnly { get; set; }

        // Shipping Details
        public double PackagedDimensions { get; set; } // Length * Width * Height
        public double PackagedWeight { get; set; }

        // FKeys
        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }

        // Digital Product Ownership
        [ForeignKey(nameof(DigitalOwnerId))]
        public int DigitalOwnerId { get; set; }
    }
}
