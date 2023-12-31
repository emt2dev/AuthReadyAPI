using AuthReadyAPI.DataLayer.DTOs.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.ProductInfo
{
    public class StyleClass
    {
        [Key]
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
        public bool UseDiscountPrice { get; set; }
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
        public StyleClass()
        {
            
        }

        public StyleClass(NewStyleDTO DTO)
        {
            Id = 0;
            Name = DTO.Name;
            Description = DTO.Description;

            CurrentPrice = DTO.CurrentPrice;
            DiscountedPrice = DTO.DiscountedPrice;
            UseDiscountPrice = DTO.UseDiscountPrice;

            CartCount = 0;
            OrderCount = 0;
            GrossIncome = 0.00;

            IsAvailableForOrder = false;
            IsLimitedTimeOnly = false;
            IsComingSoon = true;

            DigitalOnly = DTO.DigitalOnly;
            PackagedDimensions = DTO.PackagedDimensions;
            PackagedWeight = DTO.PackagedWeight;

            ProductId = DTO.ProductId;

            AvailableOn = DateTime.Now;
            UnavailableOn = DateTime.Now;
        }
    }
}
