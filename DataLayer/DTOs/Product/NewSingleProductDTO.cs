using AuthReadyAPI.DataLayer.Models.ProductInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class NewSingleProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public double Area { get; set; }
        public List<IFormFile> Images { get; set; }

        public string UserEmail { get; set; }

        public int CompanyId { get; set; }

        public double ProductPrice { get; set; }
        public double ShippingCost { get; set; }
        public string Carrier { get; set; }
        public string Delivery { get; set; }
        public string TaxCode { get; set; }
    }
}
