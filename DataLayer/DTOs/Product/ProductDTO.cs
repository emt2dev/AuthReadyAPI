using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public abstract class ProductDTO
    {
        public int Id { get; set; } // Primary Key
        // Information
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatesMade { get; set; }
        public string ImageUrl { get; set; }
        public bool DigitalOnly { get; set; }
        // Pricing
        public double CurrentPrice { get; set; }
        public int Quantity { get; set; }

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
        public double DiscountedPrice{ get; set; }
        public bool UseSalePrice { get; set; }

        // FKeys
        public string UpdatedBy { get; set; } // userId
        public int CompanyId { get; set; } // which company does the product belong to?
        public string TaxCode { get; set; }
        public int CategoryId { get; set; }
        public int StyleId { get; set; } // product style
    }
}
