using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }
        // Information
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatesMade { get; set; }
        public string MainImageUrl { get; set; }

        // Used for Cart
        public int Quantity { get; set; }

        // Metrics
        public double GrossIncome { get; set; }

        // FKeys
        public string UpdatedBy { get; set; } // userId pulled from jwt
        public int CompanyId { get; set; } // which company does the product belong to?
        public string TaxCode { get; set; }
        public int CategoryId { get; set; }
    }
}
