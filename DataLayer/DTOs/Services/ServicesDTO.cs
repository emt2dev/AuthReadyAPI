using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Services
{
    public class ServicesDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TaxCode { get; set; }
        public double CurrentPrice { get; set; }
        public int Quantity { get; set; }
        public double DiscountedPrice { get; set; }
        public bool UseDiscountedPrice { get; set; }

    }
}
