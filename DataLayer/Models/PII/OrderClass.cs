using AuthReadyAPI.DataLayer.Models.ProductInfo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.PII
{
    public class OrderClass
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }

        // Payment
        public double PaymentAmount { get; set; }
        public bool PaymentCompleted { get; set; }
        public bool PaymentRefunded { get; set; }

        // Fkeys
        public string UserEmail { get; set; }

        public string CartType { get; set; }

        [ForeignKey(nameof(ShoppingCartId))]
        public int ShoppingCartId { get; set; }

        [ForeignKey(nameof(SingleProductCartClassId))]
        public int SingleProductCartClassId { get; set; }

        [ForeignKey(nameof(AuctionProductCartClassId))]
        public int AuctionProductCartClassId { get; set; }

        [ForeignKey(nameof(ServicesCartClassId))]
        public int ServicesCartClassId { get; set; }


        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }

        // Shipping
        public List<ShippingInfoClass> ShippingInfos { get; set; }

        // Digital Product
        public List<DigitalOwnershipClass> DigitalOwnerships { get; set; }


        // Delivery Drivers
        /*
        public bool delivery { get; set; }
        public DateTime Time__Submitted { get; set; }
        public DateTime? Time__Touched { get; set; }
        public DateTime? Time__Delivered { get; set; }
        public string? delivery_driver_name { get; set; }
        public string? delivery_driver_longitdte { get; set; }
        public string? delivery_driver_latitude { get; set; }
        public string? Destination_latitude { get; set; }
        public string? Destination_longitude { get; set; }
        public string? DestinationAddress { get; set; }
        */

        public OrderClass()
        {
            
        }
    }
}
