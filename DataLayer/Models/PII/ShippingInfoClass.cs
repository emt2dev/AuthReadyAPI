using AuthReadyAPI.DataLayer.Models.ProductInfo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.PII
{
    public class ShippingInfoClass
    {
        [Key]
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
        public List<ProductClass> PackingList { get; set; }

        // Fkeys
        [ForeignKey(nameof(OrderId))]
        public int OrderId { get; set; }
        public ShippingInfoClass()
        {
            
        }
    }
}
