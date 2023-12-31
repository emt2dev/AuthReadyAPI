using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.PII.Payments
{
    public class ShippingInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Carrier { get; set; }
        public double Cost { get; set; }
        public double Area { get; set; }
        public double MaxWeight { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsFlatRate { get; set; }
        public bool IsWeighed { get; set; }
        public bool IsDigital { get; set; }
        public string DeliveryExpectation { get; set; }
        public string TrackingNumber { get; set; }
        public List<ProductWithStyleDTO> PackingList { get; set; }

        // Fkeys
        public int OrderId { get; set; }
    }
}
