using AuthReadyAPI.DataLayer.DTOs.Product;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.ProductInfo
{
    public class ProductClass
    {
        [Key]
        public int Id { get; set; } // Primary Key
        // Information
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatesMade { get; set; }
        public string MainImageUrl { get; set; }
        public string SEO { get; set; }

        // Used for Cart
        public int Quantity { get; set; }

        // Metrics
        public int ViewCount { get; set; }

        // FKeys
        [ForeignKey(nameof(UpdatedBy))]
        public string UpdatedBy { get; set; } // userId pulled from jwt
        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; } // which company does the product belong to?
        public string TaxCode { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public int CategoryId { get; set; }
        public ProductClass()
        {
            
        }

        public ProductClass(NewProductDTO DTO, string ImageUrl)
        {
            Id = 0;
            Name = DTO.Name;
            Description = DTO.Description;
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
            UpdatesMade = "New Product";
            MainImageUrl = ImageUrl;
            Quantity = 1;
            ViewCount = 0;
            UpdatedBy = "";
            CompanyId = DTO.CompanyId;
            CategoryId = DTO.CategoryId;
            TaxCode = DTO.TaxCode;
        }
    }
}
